using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    // It comes from dependency injection
    private readonly IApiVersionDescriptionProvider _descriptionProvider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider descriptionProvider)
    {
        _descriptionProvider = descriptionProvider;
    }

    // One configure method
    public void Configure(SwaggerGenOptions options)
    {
        // Iterates over all founded versions that we have in our system
        foreach (var description in _descriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo()
                {
                    Title = $"API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    // Description = , TermsOfService = , Contact = , License = 
                }
            );
        }
        
        // use fully qualified name for dto descriptions
        options.CustomSchemaIds(t => t.FullName);
        
        // for Swagger
        // include xml comments (enable creation in csproj file)
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
        
        // we get security up and running
        // swagger supports different security schemes
        // configuration here
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            // what you get in dialog
            Description = 
                "foo bar",
            // what header you will use
            Name = "Authorization",
            // where it will go
            In = ParameterLocation.Header,
            // what is the type of it
            Type = SecuritySchemeType.ApiKey,
            // the scheme for it
            Scheme = "Bearer"
        });
        
        // please use this configuration here
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                // this describes what is the security requirement
                // what shall we do and where
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    }
}