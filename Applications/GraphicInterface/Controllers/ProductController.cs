using Common.Bll.Services.Enums;
using Common.Bll.Services.Interfaces;
using Data.Dto.Models;
using GraphicInterface.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace GraphicInterface.Controllers
{
    [AdminOnly]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult ProductList()
        {
            return View();
        }

        public ActionResult GetProducts(string? text = null)
        {
            var data = _productService.Search(new BrowseProductsModel() {
                Text = text ?? string.Empty, Length = int.MaxValue, Offset = 0
            });

            return Ok(data);
        }

        [HttpGet]
        public ActionResult AddEdit([FromQuery] long? id = null)
        {
            if (id is null) return View();

            var product = _productService.GetOne(id.Value);
            
            if (product is null) return View(new AddEditProductModel());
            
            return View(new AddEditProductModel() {
                Id = product.Id,
                Name = product.Name,
                Barcode = product.Barcode,
                Price = product.Price,
                IsCountable = product.IsCountable,
                IsAgeRestricted = product.IsAgeRestricted,
                VATPercentage = product.VATPercentage
            });
        }
        
        [HttpPost]
        public ActionResult AddEdit([FromForm] AddEditProductModel model)
        {
            var result = _productService.AddEditProduct(model);

            if (result == AddEditProductResult.Ok) {
                return RedirectToAction("ProductList");
            }

            return RedirectToAction("AddEdit", new { productId = model.Id });
        }

        [HttpPost]
        public ActionResult RemoveProduct([FromForm] long productId)
        {
            _productService.RemoveProduct(productId);

            return RedirectToAction("ProductList");
        }
    }
}