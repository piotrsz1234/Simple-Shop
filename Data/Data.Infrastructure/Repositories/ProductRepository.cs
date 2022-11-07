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
        public ProductRepository(ShopContext dbContext) : base(dbContext)
        {
        }

        public IReadOnlyCollection<Product> Search(string text, int offset, int length)
        {
            try
            {
                var query = DbContext.Product.Where(x => x.IsDeleted == false);

                if (!string.IsNullOrWhiteSpace(text))
                {
                    query = query.Where(x => x.Name.Contains(text) || x.Barcode.Contains(text));
                }
                
                if(offset > 0)
                    query = query.Skip(offset);
                if(length > 0)
                    query = query.Take(length);

                var result = query.ToArray();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}