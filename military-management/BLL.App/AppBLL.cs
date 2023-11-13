using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Contracts.App;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    protected IAppUOW Uow;
    private readonly AutoMapper.IMapper _mapper;
    
    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        Uow = uow;
        _mapper = mapper;
    }

    private IMilitaryPlanService? _militaryPlans;

    public IMilitaryPlanService MilitaryPlanService =>
        _militaryPlans ??= new MilitaryPlanService(Uow, new MilitaryPlanMapper(_mapper));
    
}