using AutoMapper;
using Common.Bll.Services.Enums;
using Data.Dto.Dtos;
using Data.Dto.Models;
using Data.EF.Entities;

namespace Common.Bll.Helpers
{
    public class MapperProfile
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var result = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddEditProductModel, Product>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<Product, AddEditProductModel>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<AddEditUserModel, User>();
                cfg.CreateMap<User, AddEditUserModel>();
            });

            return result;
        }
    }
}