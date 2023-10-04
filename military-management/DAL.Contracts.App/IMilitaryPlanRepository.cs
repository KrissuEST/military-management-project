using DAL.Contracts.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.Contracts.App;

public interface IMilitaryPlanRepository : IBaseRepository<MilitaryPlan>
{
    public Task<IEnumerable<MilitaryPlan>> AllAsync(Guid userId);

    public Task<MilitaryPlan?> FindAsync(Guid id, Guid userId);
    
    public Task<IEnumerable<MilitaryPlanWithPersonCount>> AllWithPlansCountAsync(Guid userId);
}