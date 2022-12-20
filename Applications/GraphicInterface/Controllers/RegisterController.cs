using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Dto.Models;
using GraphicInterface.Attributes;
using GraphicInterface.Common;
using Microsoft.AspNetCore.Mvc;

namespace GraphicInterface.Controllers
{
    [LoggedIn]
    public class RegisterController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;

        public RegisterController(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sale([FromBody] IReadOnlyCollection<AddSaleProductModel> model)
        {
            var user = HttpContext.Session.Get<UserDto>("User");
            _saleService.SaveSale(model, user!.Id);

            return Ok(true);
        }

        [HttpGet]
        public ActionResult Scan([FromQuery] string barcode)
        {
            var data = _productService.GetOneByBarcode(barcode);

            return Ok(data);
        }
    }
}