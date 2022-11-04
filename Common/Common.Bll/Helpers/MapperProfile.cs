using AutoMapper;
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
            });

            return result;
        }
    }
}