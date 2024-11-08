using AutoMapper;
using VozAtiva.Application.DTOs;
using VozAtiva.Domain.Entities;

namespace VozAtiva.Application.DTOs.Mappings;
public class MappingProfile : Profile {
    public MappingProfile() 
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<PublicAgent, PublicAgentDTO>().ReverseMap();
        CreateMap<Alert, AlertDTO>().ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                                    .ForMember(dest => dest.PublicAgent, opt => opt.MapFrom(src => src.PublicAgent))
                                    .ReverseMap();
    }
}
