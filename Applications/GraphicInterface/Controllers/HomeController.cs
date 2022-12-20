using GraphicInterface.Attributes;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GraphicInterface.Models;

namespace GraphicInterface.Controllers;

[LoggedIn]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}