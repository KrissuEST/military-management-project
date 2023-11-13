using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class MilitaryPlanMapper : BaseMapper<BLL.DTO.MilitaryPlan, Domain.App.MilitaryPlan>
{
    public MilitaryPlanMapper(IMapper mapper) : base(mapper)
    {
    }
}