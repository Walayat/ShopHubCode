using AutoMapper;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopHub.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserAuthDto>().ReverseMap();
        }
    }
}
