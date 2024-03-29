﻿using AutoMapper;
using InOut.Domain.DTOs;
using InOut.Domain.Entities;

namespace InOut.IoC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Product, ProductDto>();
        }

        public static MapperConfiguration CreateMappingProfile()
            => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
    }
}