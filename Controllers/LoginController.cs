using Microsoft.AspNetCore.Mvc;
using Solvefy_Task.Models;
using Microsoft.AspNetCore.Http;

namespace Solvefy_Task.Controllers
{
    public class LoginController : Controller
    {
        private readonly solvefyContext context;
        public LoginController(solvefyContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult login(User user) { 
            string adminEmail = "admin123@product.com";
            string adminPassword = "admin4321";

            if (user.Email == adminEmail && user.Password == adminPassword)
            {
                HttpContext.Session.SetString("UserSession", "Admin 123");
                return RedirectToAction("Index", "Home");
            }

            var Myuser = context.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (Myuser != null)
            {
                HttpContext.Session.SetString("UserSession", Myuser.Name);
                return RedirectToAction("Index1", "Usernew");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password";
                return View(user);     }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                TempData["Sucess"] = "Registered Sucessfully";
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}
