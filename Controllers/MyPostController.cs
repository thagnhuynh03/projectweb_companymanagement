using huynhkimthang_0145_Final_LTC_.Models;
using huynhkimthang_0145_Final_LTC_.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace huynhkimthang_0145_Final_LTC_.Controllers
{
    public class MyPostController : Controller
    {
        private readonly CompanyManagementContext _context;

        public MyPostController(CompanyManagementContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var idEmp = HttpContext.Session.GetInt32("idEmp");
            var listAnouncement = await _context.Announcements
                .Include(a => a.Post)
                .Include(a => a.Post.Emp)
                .Include(a => a.AnnCate)
                .Where(a => a.Post.EmpId.Equals(idEmp))
                .ToListAsync();
            var listSchedule = await _context.Schedules
                .Include(s => s.Post)
                .Include(s => s.Post.Emp)
                .Where(s => s.Post.EmpId.Equals(idEmp))
                .ToListAsync();
            var listPost = new PostModel()
            {
                listAnnouncement = listAnouncement,
                listsShedule = listSchedule
            };
            return View(listPost);
        }
    }
}
