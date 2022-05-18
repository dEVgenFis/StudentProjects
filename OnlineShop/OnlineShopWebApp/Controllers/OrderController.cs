using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartsStorage cartsStorage;
        private readonly IOrdersStorage ordersStorage;
        public OrderController(ICartsStorage cartsStorage, IOrdersStorage ordersStorage)
        {
            this.cartsStorage = cartsStorage;
            this.ordersStorage = ordersStorage;
        }
        public IActionResult Index()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/order/index");
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View();
            }
            return View("IndexDark");
        }
        public IActionResult Buy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buy(ClientContacts clientContacts, DeliveryAddress deliveryAddress, string clientComment)
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/order/buy");
            var userCart = cartsStorage.TryGetByUserId(Constants.UserId);
            ordersStorage.AddUserOrder(Constants.UserId, userCart, clientContacts, deliveryAddress, clientComment);
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            return View();
        }
    }
}