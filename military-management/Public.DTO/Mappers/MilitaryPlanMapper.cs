using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

// BaseMapper<output, input>
public class MilitaryPlanMapper : BaseMapper<BLL.DTO.MilitaryPlan, Public.DTO.v1.MilitaryPlan>
{
    public MilitaryPlanMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public Public.DTO.v1.MilitaryPlan? MapWithCount(DAL.DTO.MilitaryPlanWithPersonCount entity)
    {
        var result = Mapper.Map<Public.DTO.v1.MilitaryPlan>(entity);
        return result;
    }
}