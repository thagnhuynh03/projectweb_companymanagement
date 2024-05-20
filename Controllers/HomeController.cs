using huynhkimthang_0145_Final_LTC_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using huynhkimthang_0145_Final_LTC_.Models.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            var announcements = await _companyManagementContext.Announcements
                .Include(a => a.Post)
                .Include(a => a.Post.Emp)
                .Include(a => a.AnnCate)
                .ToListAsync();
            var schedules = await _companyManagementContext.Schedules
                .Include(s => s.Post)
                .ToListAsync();
            var listModel = new PostModel()
            {
                listAnnouncement = announcements,
                listsShedule = schedules
            };
            
            return View(listModel);
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
