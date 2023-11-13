using AutoMapper;

namespace BLL.App;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<BLL.DTO.MilitaryPlan, Domain.App.MilitaryPlan>().ReverseMap();
    }
}