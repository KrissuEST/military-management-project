using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.EF.App;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    // String based primary keys
    public DbSet<MilitaryPlan> MilitaryPlans { get; set; } = default!; //EntityFramework takes care of it.
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    // Set up your model configuration
    protected override void OnModelCreating(ModelBuilder builder)
    {
        
    // Let the initial stuff run
    base.OnModelCreating(builder);
    
    // disable cascade delete
    foreach (var foreignKey in builder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
    {
        foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
    }
    }
}