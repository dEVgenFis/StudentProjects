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
            var userFavourites = favouritesStorage.TryGetByUserId(Constants.UserId);
            if (Constants.Theme == Theme.Light)
            {
                return View(userFavourites.Products);
            }
            return View("IndexDark", userFavourites.Products);
        }
        public IActionResult AddProduct(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var userFavourites = favouritesStorage.TryGetByUserId(Constants.UserId);
            favouritesStorage.AddProduct(product, userFavourites);
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
        public IActionResult RemoveProduct(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var userFavourites = favouritesStorage.TryGetByUserId(Constants.UserId);
            favouritesStorage.RemoveProduct(product, userFavourites);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            var userFavourites = favouritesStorage.TryGetByUserId(Constants.UserId);
            favouritesStorage.Clear(userFavourites);
            return RedirectToAction("Index");
        }
    }
}