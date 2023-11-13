using DAL.Contracts.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.Contracts.App;

public interface IMilitaryPlanRepository : IBaseRepository<MilitaryPlan>, IMilitaryPlanRepositoryCustom<MilitaryPlan>
{
    // add here custom methods for repo only
}

public interface IMilitaryPlanRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    public Task<TEntity?> FindAsync(Guid id, Guid userId);
    // Task<MilitaryPlan?> RemoveAsync(Guid id, Guid userId);
    // public - tegelt pole vaja
    // Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
    public Task<IEnumerable<MilitaryPlanWithPersonCount>> AllWithPlansCountAsync(Guid userId);
    
}