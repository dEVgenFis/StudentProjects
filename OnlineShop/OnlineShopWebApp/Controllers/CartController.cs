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
            var userCart = cartsStorage.TryGetByUserId(Constants.UserId);
            if (Constants.Theme == Theme.Light)
            {
                return View(userCart);
            }
            return View("IndexDark", userCart);
        }
        public IActionResult Add(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var userCart = cartsStorage.TryGetByUserId(Constants.UserId);
            cartsStorage.Add(product, userCart);
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
        public IActionResult DecreaseAmount(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var userCart = cartsStorage.TryGetByUserId(Constants.UserId);
            cartsStorage.DecreaseAmount(product, userCart);
            return RedirectToAction("Index");
        }
        public IActionResult RemovePosition(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var userCart = cartsStorage.TryGetByUserId(Constants.UserId);
            cartsStorage.RemovePosition(product, userCart);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            var userCart = cartsStorage.TryGetByUserId(Constants.UserId);
            cartsStorage.Clear(userCart);
            return RedirectToAction("Index");
        }
    }
}