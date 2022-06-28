using AutoMapper;
using InOut.Domain.DTOs;
using InOut.Domain.Entities;

namespace InOut.IoC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
