using huynhkimthang_0145_Final_LTC_.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace huynhkimthang_0145_Final_LTC_.Controllers
{

    public class AdminController : Controller
    {
        private readonly CompanyManagementContext _context;
        public AdminController(CompanyManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Include(e => e.Dep).Include(e => e.Role).ToListAsync();
            var role = HttpContext.Session.GetInt32("idRole"); ;
            if (role == null)
                return RedirectToAction("LogIn", "LogIn");
            else
            {
                if (role.Equals(2))
                {
                    return View(employees);
                }
                else
                {
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmpName,Email,Password,PhoneNumber,Address,Gender,DateOfBirth,Avartar,DepId,RoleId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Admin");
            }
            return View(employee);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }
    }
}
