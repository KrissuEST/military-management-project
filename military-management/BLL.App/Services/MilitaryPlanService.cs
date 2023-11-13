using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class MilitaryPlanService : 
    BaseEntityService<BLL.DTO.MilitaryPlan, Domain.App.MilitaryPlan, IMilitaryPlanRepository>, IMilitaryPlanService
{
    protected IAppUOW Uow;

    public MilitaryPlanService(IAppUOW uow, IMapper<BLL.DTO.MilitaryPlan, Domain.App.MilitaryPlan> mapper) : 
        base(uow.MilitaryPlanRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<DTO.MilitaryPlan>> AllAsync(Guid userId)
    {
        return (await Uow.MilitaryPlanRepository.AllAsync(userId)).Select(e => Mapper.Map(e));
    }

    public async Task<DTO.MilitaryPlan?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.MilitaryPlanRepository.FindAsync(id, userId));
    }

    public async Task<IEnumerable<MilitaryPlanWithPersonCount>> AllWithPlansCountAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}