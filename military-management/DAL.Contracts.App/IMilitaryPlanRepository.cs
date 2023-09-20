using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IMilitaryPlanRepository : IBaseRepository<MilitaryPlan>
{
    public Task<IEnumerable<MilitaryPlan>> AllAsync(Guid userId);
}