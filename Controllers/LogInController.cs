using huynhkimthang_0145_Final_LTC_.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace huynhkimthang_0145_Final_LTC_.Controllers
{
    public class LogInController : Controller
    {
        private readonly CompanyManagementContext _context;

        public LogInController(CompanyManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(Employee employee)
        {
            if(ModelState.IsValid)
            {

                var data = _context.Employees
                    .Where(e => e.Email.Equals(employee.Email) && e.Password.Equals(employee.Password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    HttpContext.Session.SetString("FullName", data.FirstOrDefault().EmpName);
                    HttpContext.Session.SetString("Img", data.FirstOrDefault().Avartar);
                    HttpContext.Session.SetInt32("idEmp", data.FirstOrDefault().EmpId);
                    int? roleId = data.FirstOrDefault()?.RoleId;
                    if (roleId.HasValue)
                    {
                        HttpContext.Session.SetInt32("idRole", roleId.Value);
                    }
                    else
                    {
                        throw new InvalidOperationException("RoleId is null.");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Username or password is wrong!";
                    return View();
                }
            }
            return View();
        }
    }
}
