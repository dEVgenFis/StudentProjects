using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IFavouritesStorage favouritesStorage;
        public FavouritesController(IProductsStorage productsStorage, IFavouritesStorage favouritesStorage)
        {
            this.productsStorage = productsStorage;
            this.favouritesStorage = favouritesStorage;
        }
        public IActionResult Index()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/favourites/index");
            var item = favouritesStorage.TryGetByUserId(Constants.UserId);
            if (Constants.Theme == Theme.Light)
            {
                return View(item.Products);
            }
            return View("IndexDark", item.Products);
        }
        public IActionResult AddProduct(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            favouritesStorage.AddProduct(product, Constants.UserId);
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
        public IActionResult RemoveProduct(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            favouritesStorage.RemoveProduct(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            favouritesStorage.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}