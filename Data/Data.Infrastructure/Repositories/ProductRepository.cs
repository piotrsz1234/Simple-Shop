using Data.EF.Contexts;
using Data.EF.Entities;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Data.Infrastructure.Repositories
{
    public sealed class ProductRepository : RepositoryGenericBase<Product>, IProductRepository
    {
        public ProductRepository(ShopContext dbContext, ILogger<ProductRepository> logger) : base(dbContext, logger)
        {
        }
    }
}