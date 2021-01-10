﻿using AutoMapper;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Models;

namespace WalletSystemAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
        }
    }
}