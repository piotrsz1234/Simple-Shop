using System.Collections.Generic;
using System.Threading.Tasks;
using Data.EF.Entities;

namespace Data.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryGenericBase<Product>
    {
        Task<IReadOnlyCollection<Product>> SearchAsync(string text, int offset, int length);
    }
}