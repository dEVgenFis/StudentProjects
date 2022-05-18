using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/users/index");
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View();
            }
            return View("IndexDark");
        }
    }
}