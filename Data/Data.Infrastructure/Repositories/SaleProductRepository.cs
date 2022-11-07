using Data.EF.Contexts;
using Data.EF.Entities;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Data.Infrastructure.Repositories
{
    public sealed class SaleProductRepository : RepositoryGenericBase<SaleProduct>, ISaleProductRepository
    {
        public SaleProductRepository(ShopContext dbContext) : base(dbContext)
        {
        }
    }
}