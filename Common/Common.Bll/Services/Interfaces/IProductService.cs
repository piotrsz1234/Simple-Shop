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
        IReadOnlyCollection<ProductDto>? Search(BrowseProductsModel model);
        Task<ProductDto?> GetOneByBarcodeAsync(string barcode);
    }
}