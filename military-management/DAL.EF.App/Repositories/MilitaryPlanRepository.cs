using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class MilitaryPlanRepository : 
    EFBaseRepository<MilitaryPlan, ApplicationDbContext>, IMilitaryPlanRepository  // Implements IMilitaryPlanRepository
{
    public MilitaryPlanRepository(ApplicationDbContext dataContext) : base(dataContext)  // Constructor
    {
    }

    public override async Task<IEnumerable<MilitaryPlan>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.AppUser)
            .OrderBy(e => e.PlanName)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<MilitaryPlan>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.AppUser)
            .Include(t => t.PlanPersons)
            .OrderBy(e => e.PlanName)
            .Where(t => t.AppUserId == userId)
            .ToListAsync();
    }

    public override async Task<MilitaryPlan?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(m => m.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public virtual async Task<MilitaryPlan?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(m => m.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);
    }

    public async Task<IEnumerable<MilitaryPlanWithPersonCount>> AllWithPlansCountAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.AppUser)
            .Include(t => t.PlanPersons)
            .OrderBy(e => e.PlanName)
            .Where(t => t.AppUserId == userId)
            .Select(t => new MilitaryPlanWithPersonCount()
            {
                Id = t.Id,
                PlanName = t.PlanName,
                PersonCount = t.PlanPersons!.Count
            })
            .ToListAsync();
    }
}