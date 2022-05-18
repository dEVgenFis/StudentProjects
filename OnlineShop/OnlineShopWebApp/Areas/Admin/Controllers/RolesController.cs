using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly IRolesStorage rolesStorage;
        public RolesController(IRolesStorage rolesStorage)
        {
            this.rolesStorage = rolesStorage;
        }
        public IActionResult Index()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/roles/index");
            var roles = rolesStorage.GetAllRoles();
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View(roles);
            }
            return View("IndexDark", roles);
        }
        public IActionResult Add()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/roles/add");
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View();
            }
            return View("AddDark");
        }

        [HttpPost]
        public IActionResult Add(Role role)
        {
            if (rolesStorage.TryGetByRoleName(role.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует!");
            }
            if (ModelState.IsValid)
            {
                rolesStorage.AddRole(role);
                return RedirectToAction("Index");
            }
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View("Add", role);
            }
            return View("AddDark", role);
        }

        public IActionResult Remove(string roleName)
        {
            var role = rolesStorage.TryGetByRoleName(roleName);
            rolesStorage.RemoveRole(role);
            return RedirectToAction("Index");
        }
    }
}