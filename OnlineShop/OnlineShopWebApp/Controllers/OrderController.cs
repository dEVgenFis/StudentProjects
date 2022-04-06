using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Collections.Generic;

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
            if (Constants.Theme == Theme.Light)
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
        public IActionResult Buy(ClientContacts clientContacts, DeliveryAddress deliveryAddress, string comment)
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/order/buy");
            var cart = cartsStorage.TryGetByUserId(Constants.UserId);
            var item = new Order
            {
                UserId = Constants.UserId,
                ClientContacts = clientContacts,
                DeliveryAddress = deliveryAddress,
                Comment = comment,
                Cart = new Cart
                {
                    UserId = Constants.UserId,
                    Items = new List<CartItem>(cart.Items)
                }
            };
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            ordersStorage.AddOrder(item);
            cart.Items.Clear();
            return View();
        }
    }
}