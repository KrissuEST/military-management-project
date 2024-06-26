using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using BLL.App;
using BLL.Contracts.App;
using DAL.Contracts.App;
using DAL.EF.App;
using DAL.EF.App.Seeding;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// Here is dependency injection engine
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));   // Here not using UseSqlite but UseNpgsql, it kicked error away.

// Register our UOW with scoped lifecycle.
// IAppUOW will be added to injection dependency engine with scoped lifecycle
// and tells to system when IAppUOW is asked pls go and create AppUOW. 
// Actual implementation comes from AppUOW.
builder.Services.AddScoped<IAppUOW, AppUOW>();
builder.Services.AddScoped<IAppBLL, AppBLL>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<AppUser, AppRole>(
        options => options.SignIn.RequireConfirmedAccount = false
        )
    //.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// our custom stuff here
builder.Services
    .AddAuthentication()
    .AddCookie(options => { options.SlidingExpiration = true; })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer")!,  // true by default
            ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience")!, // true by default
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Key")!)),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddControllersWithViews();

// for two different localhosts, that they can communicate with each other
// routing here
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsAllowAll", policy =>
    {
        policy.AllowAnyHeader();  // headers control
        policy.AllowAnyMethod();  // post, get, put control
        policy.AllowAnyOrigin();  // host control
    });
});

// add automapper configurations
builder.Services.AddAutoMapper(
    typeof(BLL.App.AutoMapperConfig),
    typeof(Public.DTO.AutoMapperConfig)
);


var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    // in case of no explicit version
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

apiVersioningBuilder.AddApiExplorer(options =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

// Dependency injection stuff and setting up the system


// ===================================
var app = builder.Build();   //middle point of configuration and setting up the system
// ===================================


// Webserver set up, set up database stuff and seed initial data

SetupAppData(app, app.Environment, app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsAllowAll");

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName
            );
        }
    });
//Area juures uus
// app.MapControllerRoute(
//     name: "area",
//     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Start up the web server and wait for requests.
app.Run();

static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment environment, IConfiguration configuration)
{
    // using - we don't need to use dispose, fancy technique
    // using - when method ends, it's automatically disposed to us
    // Dependency injection engine here
    using var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    
    //DB check, if there is no DB
    if (context == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize DB Context.");
    }

    using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
    using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
    
    if (userManager == null || roleManager == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize UserManager or RoleManager.");
    }

    // logging stuff out correctly
    var logger = serviceScope.ServiceProvider.GetService<ILogger<IApplicationBuilder>>();
    if (logger == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize logger.");
    }
    // If database is InMemory type.
    if (context.Database.ProviderName!.Contains("InMemory"))
    {
        return;   // Don't do anything.
    }
    
    // TODO: wait for DB connection
    
    // Drop DB?
    if (configuration.GetValue<bool>("DataInit:DropDatabase"))
    {
        logger.LogWarning("Dropping database");
        // Calling methods out from AppDataInit.cs
        AppDataInit.DropDatabase(context);
    }

    // Migrate DB?
    if (configuration.GetValue<bool>("DataInit:MigrateDatabase"))
    {
        logger.LogInformation("Migrating database");
        AppDataInit.MigrateDatabase(context);
    }

    // Seed identity?
    if (configuration.GetValue<bool>("DataInit:SeedIdentity"))
    {
        logger.LogInformation("Seeding identity");
        AppDataInit.SeedIdentity(userManager, roleManager);
    }

    // Seed application data?
    if (configuration.GetValue<bool>("DataInit:SeedData"))
    {
        logger.LogInformation("Seed app data");
        AppDataInit.SeedAppData(context);
    }
}

public partial class Program
{
}
