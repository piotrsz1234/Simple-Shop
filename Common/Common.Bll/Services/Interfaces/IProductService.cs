using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Dto.Dtos;
using Data.Dto.Models;

namespace Common.Bll.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddEditProductAsync(AddEditProductModel model);
        Task<bool> RemoveProductAsync(long productId);
        Task<IReadOnlyCollection<ProductDto>?> SearchAsync(BrowseProductsModel model);
    }
}