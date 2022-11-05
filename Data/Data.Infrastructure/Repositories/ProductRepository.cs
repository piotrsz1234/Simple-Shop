using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.EF.Contexts;
using Data.EF.Entities;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Infrastructure.Repositories
{
    public sealed class ProductRepository : RepositoryGenericBase<Product>, IProductRepository
    {
        public ProductRepository(ShopContext dbContext, ILogger<ProductRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<IReadOnlyCollection<Product>> SearchAsync(string text, int offset, int length)
        {
            try
            {
                var query = DbContext.Product.Where(x => x.IsDeleted == false);

                if (!string.IsNullOrWhiteSpace(text))
                {
                    query = query.Where(x => x.Name.Contains(text) || x.Barcode.Contains(text));
                }

                query = query.Skip(offset).Take(length);

                return await query.ToArrayAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}