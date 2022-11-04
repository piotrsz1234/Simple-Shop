using Data.EF.Contexts;
using Data.EF.Entities;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Data.Infrastructure.Repositories
{
    public sealed class SaleRepository : RepositoryGenericBase<Sale>, ISaleRepository
    {
        public SaleRepository(ShopContext dbContext, ILogger<SaleRepository> logger) : base(dbContext, logger)
        {
        }
    }
}