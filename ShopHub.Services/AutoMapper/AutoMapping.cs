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
        /*This method we are using to register our model class to specifc mapping dto for mapping purpose*/
        public AutoMapping()
        {
            CreateMap<User, UserAuthDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
