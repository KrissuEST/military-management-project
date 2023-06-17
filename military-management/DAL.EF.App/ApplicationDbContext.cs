using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    // String based primary keys
    public DbSet<MilitaryActivity> MilitaryActivities { get; set; } = default!; //EntityFramework takes care of it.
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}