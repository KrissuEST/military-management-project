using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IMilitaryPlanService : IBaseRepository<BLL.DTO.MilitaryPlan>, IMilitaryPlanRepositoryCustom<BLL.DTO.MilitaryPlan>
{
    // add your custom service methods here
}