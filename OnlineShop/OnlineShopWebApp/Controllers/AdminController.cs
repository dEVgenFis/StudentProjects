using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IOrdersStorage ordersStorage;
        private readonly IRolesStorage rolesStorage;
        private readonly IWorkLocationsStorage locationsStorage;
        public AdminController(IProductsStorage productsStorage, IOrdersStorage ordersStorage, IRolesStorage rolesStorage, IWorkLocationsStorage locationsStorage)
        {
            this.productsStorage = productsStorage;
            this.ordersStorage = ordersStorage;
            this.rolesStorage = rolesStorage;
            this.locationsStorage = locationsStorage;
        }
        public IActionResult Orders()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/orders");
            var orders = ordersStorage.GetAllOrders();
            if (Constants.Theme == Theme.Light)
            {
                return View(orders);
            }
            return View("ordersdark", orders);
        }
        public IActionResult OrderDetails(Guid orderId)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/admin/orderdetails?orderId={orderId}");
            var order = ordersStorage.TryGetByOrderId(orderId);
            if (Constants.Theme == Theme.Light)
            {
                return View(order);
            }
            return View("orderdetailsdark", order);
        }

        [HttpPost]
        public IActionResult UpdateOrdrerStatus(Guid orderId, OrderStatuses status)
        {
            ordersStorage.UpdateOrderStatus(orderId, status);
            return RedirectToAction("orders");
        }

        public IActionResult Users()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/users");
            if (Constants.Theme == Theme.Light)
            {
                return View();
            }
            return View("usersdark");
        }
        public IActionResult Roles()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/roles");
            var roles = rolesStorage.GetAllRoles();
            if (Constants.Theme == Theme.Light)
            {
                return View(roles);
            }
            return View("rolesdark", roles);
        }
        public IActionResult AddRole()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/addrole");
            if (Constants.Theme == Theme.Light)
            {
                return View();
            }
            return View("addroledark");
        }

        [HttpPost]
        public IActionResult AddRole(Role role)
        {
            if (rolesStorage.TryGetByRoleName(role.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует!");
            }
            if (ModelState.IsValid)
            {
                rolesStorage.AddRole(role);
                return RedirectToAction("roles");
            }
            if (Constants.Theme == Theme.Light)
            {
                return View("addrole", role);
            }
            return View("addroledark", role);
        }

        public IActionResult RemoveRole(string roleName)
        {
            rolesStorage.RemoveRole(roleName);
            return RedirectToAction("roles");
        }
        public IActionResult Products()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/products");
            var сatalog = productsStorage.GetAllProducts();
            if (Constants.Theme == Theme.Light)
            {
                return View(сatalog);
            }
            return View("productsdark", сatalog);
        }
        public IActionResult AddProduct()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/addproduct");
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            if (Constants.Theme == Theme.Light)
            {
                return View();
            }
            return View("addproductdark");
        }

        [HttpPost]
        public IActionResult AddProduct(Product product, string[] locations)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            productsStorage.AddWorkLocations(product, locations);
            productsStorage.AddProduct(product);
            return RedirectToAction("products");
        }

        public IActionResult EditProduct(Guid productId)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/admin/editproduct?productId={productId}");
            var product = productsStorage.TryGetProductById(productId);
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            if (Constants.Theme == Theme.Light)
            {
                return View(product);
            }
            return View("editproductdark", product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product, string[] locations)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            productsStorage.AddWorkLocations(product, locations);
            productsStorage.UpdateProduct(product);
            return RedirectToAction("products");
        }

        public IActionResult RemoveProduct(Guid productId)
        {
            productsStorage.RemoveProduct(productId);
            return RedirectToAction("products");
        }
    }
}