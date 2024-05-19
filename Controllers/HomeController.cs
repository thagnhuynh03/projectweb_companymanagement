using huynhkimthang_0145_Final_LTC_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using huynhkimthang_0145_Final_LTC_.Models.Entities;

namespace huynhkimthang_0145_Final_LTC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompanyManagementContext _companyManagementContext;

        public HomeController(ILogger<HomeController> logger, CompanyManagementContext companyManagementContext)
        {
            _logger = logger;
            _companyManagementContext = companyManagementContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
