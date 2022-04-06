using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly ICartsStorage cartsStorage;
        public CartController(IProductsStorage productsStorage, ICartsStorage cartsStorage)
        {
            this.productsStorage = productsStorage;
            this.cartsStorage = cartsStorage;
        }
        public IActionResult Index()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/cart/index");
            var cart = cartsStorage.TryGetByUserId(Constants.UserId);
            if (Constants.Theme == Theme.Light)
            {
                return View(cart);
            }
            return View("IndexDark", cart);
        }
        public IActionResult Add(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            cartsStorage.Add(product, Constants.UserId);
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
        public IActionResult DecreaseAmount(Guid productId)
        {
            cartsStorage.DecreaseAmount(productId, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult RemovePosition(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            cartsStorage.RemovePosition(productId, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            cartsStorage.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}