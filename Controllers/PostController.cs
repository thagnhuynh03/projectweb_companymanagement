using Microsoft.AspNetCore.Mvc;
using huynhkimthang_0145_Final_LTC_.Models;
using huynhkimthang_0145_Final_LTC_.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;

namespace huynhkimthang_0145_Final_LTC_.Controllers
{
    public class PostController : Controller
    {
        private readonly CompanyManagementContext _context;

        public PostController(CompanyManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Search(string query)
        {
            var announcements = await _context.Announcements
                .Include(a => a.Post)
                .Include(a => a.Post.Emp)
                .Include(a => a.Post.PostTypeNavigation)
                .Include(a => a.AnnCate)
                .Where(a => a.Post.Title.Contains(query) || a.Post.Content.Contains(query) 
                || a.Post.PostTypeNavigation.PostCateName.Contains(query)
                || a.Post.Emp.EmpName.Contains(query) || a.AnnCate.AnnCateName.Contains(query))
                .ToListAsync();
            var schedules = await _context.Schedules
                .Include(a => a.Post)
                .Include(a => a.Post.Emp)
                .Include(a => a.Post.PostTypeNavigation)
                .Where(s => s.Post.Title.Contains(query) || s.Post.Content.Contains(query)
                || s.Post.PostTypeNavigation.PostCateName.Contains(query)
                || s.Post.Emp.EmpName.Contains(query) || s.Loacation.Contains(query))
                .ToListAsync();
            var listModel = new PostModel()
            {
                listAnnouncement = announcements,
                listsShedule = schedules
            };
            return View(listModel);
        }
    }
}
