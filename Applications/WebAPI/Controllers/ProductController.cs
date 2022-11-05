using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddEditProduct([FromForm] AddEditProductModel model) =>
            Ok(await _productService.AddEditProductAsync(model));

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProduct([FromForm] DeleteProductModel model) =>
            Ok(await _productService.RemoveProductAsync(model.Id));

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<ProductDto>>> Search([FromQuery(Name = "")] BrowseProductsModel model) => 
            Ok(await _productService.SearchAsync(model));
    }
}