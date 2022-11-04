using System.Threading.Tasks;
using Common.Bll.Services.Interfaces;
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

        public async Task<ActionResult<bool>> AddEditProduct([FromForm] AddEditProductModel model) =>
            Ok(await _productService.AddEditProductAsync(model));
    }
}