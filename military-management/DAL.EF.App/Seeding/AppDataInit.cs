using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Seeding;

public static class AppDataInit
{
    private static Guid adminId = Guid.Parse("1eb6ca21-9f2e-4de2-ac43-e14c40f0f1df");
    // Drop DB method
    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }
    
    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }
    
    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        (Guid id, string email, string pwd) userData = (adminId,"admin@app.com", "Foo.bar.1");
        var user = userManager.FindByEmailAsync(userData.email).Result;
        
        // if we didn't get user, then we need to create it.
        if (user == null)
        {
            user = new AppUser() //our new AppUser with the data what we need
            {
                Id = userData.id,
                Email = userData.email,
                UserName = userData.email,
                EmailConfirmed = true,
            };
            //creating that user
            var result = userManager.CreateAsync(user, userData.pwd).Result; //result immediately
            if (!result.Succeeded)
            {
                throw new ApplicationException(message: "Cannot seed users");  
                        //(message: $"Cannot seed users, {result.ToString()}"); - nii saab logida
            }
        }
    }
    
    public static void SeedAppData(ApplicationDbContext context)
    {
        SeedAppDataMilitaryPlan(context);
        context.SaveChanges();
    }
    
    private static void SeedAppDataMilitaryPlan(ApplicationDbContext context)
    {
        if (context.MilitaryPlans.Any()) return;

        context.MilitaryPlans.Add(new MilitaryPlan()
            {
                PlanName = "Test Plan",
                AppUserId = adminId
            }
        );
    }

}