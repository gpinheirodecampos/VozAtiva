using AutoMapper;
using VozAtiva.Application.DTOs;
using VozAtiva.Domain.Entities;

namespace Rents.Application.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}