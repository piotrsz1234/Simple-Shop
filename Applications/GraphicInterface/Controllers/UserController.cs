using Common.Bll.Services.Enums;
using Common.Bll.Services.Interfaces;
using Data.Dto.Models;
using GraphicInterface.Attributes;
using GraphicInterface.Common;
using GraphicInterface.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GraphicInterface.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [OnlyAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [OnlyAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var result = await _userService.LoginUserAsync(model.Code);

            if (result is null) return View();
            
            HttpContext.Session.Set("User", result);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [LoggedIn]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AdminOnly]
        public ActionResult UserList()
        {
            return View();
        }        
        
        [HttpGet]
        [AdminOnly]
        public ActionResult GetUsers([FromQuery] string? text = null)
        {
            var data = _userService.Search(text ?? string.Empty);

            return Ok(data);
        }

        [HttpPost]
        [AdminOnly]
        public ActionResult RemoveUser([FromForm] RemoveUserViewModel model)
        {
            _userService.RemoveUser(model.UserId);

            return RedirectToAction("UserList");
        }
        
        [HttpGet]
        [AdminOnly]
        public ActionResult AddEdit([FromQuery] long? id = null)
        {
            var data = id.HasValue ? _userService.GetOne(id.Value) : null;

            if (data is null) return View(new AddEditUserModel());
            
            return View(new AddEditUserModel() {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                IsAdmin = data.IsAdmin,
                LoginCode = data.LoginCode
            });
        }

        [HttpPost]
        [AdminOnly]
        public ActionResult AddEdit([FromForm] AddEditUserModel model)
        {
            var result = _userService.AddEditUser(model);

            if (result == AddEditUserResult.Ok) {
                return RedirectToAction("UserList");
            }

            return View();
        }
    }
}