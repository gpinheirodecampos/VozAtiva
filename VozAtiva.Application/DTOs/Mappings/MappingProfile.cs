using AutoMapper;
using VozAtiva.Application.DTOs;
using VozAtiva.Domain.Entities;

namespace VozAtiva.Application.DTOs.Mappings;
public class MappingProfile : Profile {
    public MappingProfile() 
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<PublicAgent, PublicAgentDTO>().ReverseMap();
    }
}
