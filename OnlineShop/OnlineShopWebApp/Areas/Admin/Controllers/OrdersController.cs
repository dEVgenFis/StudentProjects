using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Models;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Areas.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrdersStorage ordersStorage;
        public OrdersController(IOrdersStorage ordersStorage)
        {
            this.ordersStorage = ordersStorage;
        }
        public IActionResult Index()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/orders/index");
            var orders = ordersStorage.GetAllOrders();
            if (Constants.Theme == Theme.Light)
            {
                return View(orders);
            }
            return View("IndexDark", orders);
        }
        public IActionResult Details(Guid orderId)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/admin/orders/details?orderId={orderId}");
            var order = ordersStorage.TryGetByOrderId(orderId);
            if (Constants.Theme == Theme.Light)
            {
                return View(order);
            }
            return View("DetailsDark", order);
        }

        [HttpPost]
        public IActionResult UpdateStatus(Guid orderId, OrderStatuses status)
        {
            ordersStorage.UpdateOrderStatus(orderId, status);
            return RedirectToAction("Index");
        }
    }
}