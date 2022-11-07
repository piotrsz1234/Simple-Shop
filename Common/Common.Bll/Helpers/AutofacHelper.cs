using Common.Bll.Services;
using Common.Bll.Services.Interfaces;
using Data.Infrastructure.Repositories;
using Data.Infrastructure.Repositories.Interfaces;

namespace Common.Bll.Helpers
{
    public static class AutofacHelper
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            return new Dictionary<Type, Type> {
                { typeof(IProductRepository), typeof(ProductRepository) },
                { typeof(ISaleRepository), typeof(SaleRepository) },
                { typeof(ISaleProductRepository), typeof(SaleProductRepository) },
                { typeof(ISaleService), typeof(SaleService) },
                { typeof(IProductService), typeof(ProductService) },
                { typeof(IUserRepository), typeof(UserRepository) },
                { typeof(IUserService), typeof(UserService) },
                
            };
        }
    }
}