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
            ViewBag.Categories = _context.AnnouncementCategories.ToList();
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
        [HttpGet]
        public IActionResult UpdateAnouncementPost(int id)
        {
            ViewBag.Categories = _context.AnnouncementCategories.ToList();
            var anouncement = _context.Announcements
            .Include(a => a.Post)
            .FirstOrDefault(a => a.AnnId == id);
            if (anouncement == null || anouncement.Post == null)
            {
                return RedirectToAction("Index", "MyPost");
            }
            var model = new AnouncementViewModel()
            {
                Title = anouncement.Post.Title,
                Content = anouncement.Post.Content,
                AnnCateId = anouncement.AnnCateId
            };

            ViewData["AnnId"] = anouncement.AnnId;
            ViewData["ImgFileName"] = anouncement.Post.ThumbnailImg;
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateAnouncementPost(int id, AnouncementViewModel model)
        {
            var anouncement = _context.Announcements
            .Include(a => a.Post)
            .FirstOrDefault(a => a.AnnId == id);
            if (anouncement == null || anouncement.Post == null)
            {
                return RedirectToAction("Index", "MyPost");
            }

            string newFileName = anouncement.Post.ThumbnailImg;
            if (model.ThumbnailImg != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(model.ThumbnailImg.FileName);

                string imageFullPath = _environment.WebRootPath + "/Img/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    model.ThumbnailImg.CopyTo(stream);
                }

                string oldImageFullPath = _environment.WebRootPath + "/Img/" + anouncement.Post.ThumbnailImg;
                System.IO.File.Delete(oldImageFullPath);
            }

            anouncement.Post.Title = model.Title;
            anouncement.Post.Content = model.Content;
            anouncement.Post.ThumbnailImg = newFileName;
            anouncement.AnnCateId = model.AnnCateId;

            _context.SaveChanges();
            return RedirectToAction("Index", "MyPost");
        }

        [HttpGet]
        public IActionResult UpdateSchedulePost(int id)
        {
            var schedule = _context.Schedules
            .Include(a => a.Post)
            .FirstOrDefault(a => a.SchId == id);
            if (schedule == null || schedule.Post == null)
            {
                return RedirectToAction("Index", "MyPost");
            }
            var model = new ScheduleViewModel()
            {
                Title = schedule.Post.Title,
                Content = schedule.Post.Content,
                Time = schedule.StartTime,
                Location = schedule.Loacation
            };

            ViewData["SchId"] = schedule.SchId;
            ViewData["ImgFileName"] = schedule.Post.ThumbnailImg;
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateSchedulePost(int id, ScheduleViewModel model)
        {
            var schedule = _context.Schedules
            .Include(a => a.Post)
            .FirstOrDefault(a => a.SchId == id);
            if (schedule == null || schedule.Post == null)
            {
                return RedirectToAction("Index", "MyPost");
            }

            string newFileName = schedule.Post.ThumbnailImg;
            if (model.ThumbnailImg != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(model.ThumbnailImg.FileName);

                string imageFullPath = _environment.WebRootPath + "/Img/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    model.ThumbnailImg.CopyTo(stream);
                }

                string oldImageFullPath = _environment.WebRootPath + "/Img/" + schedule.Post.ThumbnailImg;
                System.IO.File.Delete(oldImageFullPath);
            }

            schedule.Post.Title = model.Title;
            schedule.Post.Content = model.Content;
            schedule.Post.ThumbnailImg = newFileName;
            schedule.StartTime = model.Time; ;
            schedule.Loacation = model.Location;

            _context.SaveChanges();
            return RedirectToAction("Index", "MyPost");
        }

        public IActionResult DeleteAnouncementPost(int id)
        {
            var anouncement = _context.Announcements
            .Include(a => a.Post)
            .FirstOrDefault(a => a.AnnId == id);
            if (anouncement == null || anouncement.Post == null)
            {
                return RedirectToAction("Index", "MyPost");
            }

            string oldImageFullPath = _environment.WebRootPath + "/Img/" + anouncement.Post.ThumbnailImg;
            System.IO.File.Delete(oldImageFullPath);
            var post = _context.Posts.Find(anouncement.PostId);
            _context.Posts.Remove(post);
            _context.Announcements.Remove(anouncement);
            _context.SaveChanges();
            return RedirectToAction("Index", "MyPost");
        }

        public IActionResult DeleteSchedulePost(int id)
        {
            var schedule = _context.Schedules
            .Include(a => a.Post)
            .FirstOrDefault(a => a.SchId == id);
            if (schedule == null || schedule.Post == null)
            {
                return RedirectToAction("Index", "MyPost");
            }

            string oldImageFullPath = _environment.WebRootPath + "/Img/" + schedule.Post.ThumbnailImg;
            System.IO.File.Delete(oldImageFullPath);
            var post = _context.Posts.Find(schedule.PostId);
            _context.Posts.Remove(post);
            _context.Schedules.Remove(schedule);
            _context.SaveChanges();
            return RedirectToAction("Index", "MyPost");
        }
    }
}
