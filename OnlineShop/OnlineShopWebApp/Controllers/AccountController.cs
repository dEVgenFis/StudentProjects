using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/account/login");
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View();
            }
            return View("LoginDark");
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }

        public IActionResult Register()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/account/register");
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View();
            }
            return View("RegisterDark");
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (register.UserName == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View(register);
            }
            return View("RegisterDark", register);
        }
    }
}