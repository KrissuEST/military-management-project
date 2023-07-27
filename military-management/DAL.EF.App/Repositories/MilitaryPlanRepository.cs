using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class MilitaryPlanRepository : EFBaseRepository<MilitaryPlan, ApplicationDbContext>, IMilitaryPlanRepository  // Implements IMilitaryPlanRepository
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

    public override async Task<MilitaryPlan?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(m => m.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}