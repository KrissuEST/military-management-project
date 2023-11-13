using AutoMapper;

namespace Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        // Map from this to this and other way around.
        CreateMap<BLL.DTO.MilitaryPlan, Public.DTO.v1.MilitaryPlan>()
            .ForMember(
                dest => dest.PersonCount,
                options => 
                    options.MapFrom(src => src.PlanPersons!.Count)
                );
        
        CreateMap<DAL.DTO.MilitaryPlanWithPersonCount, Public.DTO.v1.MilitaryPlan>();
    }
}