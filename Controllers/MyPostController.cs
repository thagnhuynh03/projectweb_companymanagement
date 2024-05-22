using huynhkimthang_0145_Final_LTC_.Models;
using huynhkimthang_0145_Final_LTC_.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace huynhkimthang_0145_Final_LTC_.Controllers
{
    public class MyPostController : Controller
    {
        private readonly CompanyManagementContext _context;
        private readonly IWebHostEnvironment _environment;

        public MyPostController(CompanyManagementContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
        public async Task<IActionResult> MyAnouncementPost()
        {
            var idEmp = HttpContext.Session.GetInt32("idEmp");
            var announcements = await _context.Announcements
                .Include(a => a.Post)
                .Include(a => a.Post.Emp)
                .Include(a => a.AnnCate)
                .Where(a => a.Post.EmpId.Equals(idEmp))
                .ToListAsync();
            return View(announcements);
        }
        public async Task<IActionResult> MySchedulePost()
        {
            var idEmp = HttpContext.Session.GetInt32("idEmp");
            var schedules = await _context.Schedules
                .Include(s => s.Post)
                .Include(s => s.Post.Emp)
                .Where(s => s.Post.EmpId.Equals(idEmp))
                .ToListAsync();
            return View(schedules);
        }
        [HttpGet]
        public IActionResult CreateAnouncementPost()
        {
            ViewBag.Categories = _context.AnnouncementCategories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateAnouncementPost(AnouncementViewModel model)
        {
            if (model.ThumbnailImg == null)
            {
                ModelState.AddModelError("ThumbnailImg", "The image file is required");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(model.ThumbnailImg!.FileName);

            string imageFullPath = _environment.WebRootPath + "/Img/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                model.ThumbnailImg.CopyTo(stream);
            }

            Post post = new Post();
            post.Title = model.Title;
            post.Content = model.Content;
            post.ThumbnailImg = newFileName;
            post.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
            post.PostType = 1;
            post.EmpId = HttpContext.Session.GetInt32("idEmp");

            _context.Posts.Add(post);
            _context.SaveChanges();

            Announcement announcement = new Announcement();
            announcement.PostId = post.PostId;
            announcement.AnnCateId = model.AnnCateId;
            _context.Announcements.Add(announcement);
            _context.SaveChanges();


            return RedirectToAction("Index", "MyPost");
        }

        [HttpGet]
        public IActionResult CreateSchedulePost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSchedulePost(ScheduleViewModel model)
        {
            if (model.ThumbnailImg == null)
            {
                ModelState.AddModelError("ThumbnailImg", "The image file is required");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(model.ThumbnailImg!.FileName);

            string imageFullPath = _environment.WebRootPath + "/Img/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                model.ThumbnailImg.CopyTo(stream);
            }
            Post post = new Post();
            post.Title = model.Title;
            post.Content = model.Content;
            post.ThumbnailImg = newFileName;
            post.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
            post.PostType = 2;
            post.EmpId = HttpContext.Session.GetInt32("idEmp");

            _context.Posts.Add(post);
            _context.SaveChanges();

            Schedule schedule = new Schedule();
            schedule.PostId = post.PostId;
            schedule.StartTime = model.Time;
            schedule.Loacation = model.Location;
            _context.Schedules.Add(schedule);
            _context.SaveChanges();

            return RedirectToAction("Index", "MyPost");
        }
    }
}
