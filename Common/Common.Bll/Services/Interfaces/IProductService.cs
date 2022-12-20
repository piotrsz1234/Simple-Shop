using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Bll.Services.Enums;
using Data.Dto.Dtos;
using Data.Dto.Models;

namespace Common.Bll.Services.Interfaces
{
    public interface IProductService
    {
        Task<AddEditProductResult> AddEditProductAsync(AddEditProductModel model);
        Task<bool> RemoveProductAsync(long productId);
        bool RemoveProduct(long productId);
        IReadOnlyCollection<ProductDto>? Search(BrowseProductsModel model);
        ProductDto? GetOneByBarcode(string barcode);
        AddEditProductResult AddEditProduct(AddEditProductModel model);
        ProductDto? GetOne(long productId);
    }
}