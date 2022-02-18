using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class ThemeController : Controller
    {
        public IActionResult UpdateThemeOnDark()
        {
            Constants.Theme = Theme.Dark;
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
        public IActionResult UpdateThemeOnLight()
        {
            Constants.Theme = Theme.Light;
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
    }
}