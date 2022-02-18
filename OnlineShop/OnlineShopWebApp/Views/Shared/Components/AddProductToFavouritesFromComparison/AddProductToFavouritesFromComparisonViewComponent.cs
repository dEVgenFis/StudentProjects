using Microsoft.AspNetCore.Mvc;
using System;

namespace OnlineShopWebApp.Views.Shared.Components.AddProductToFavouritesFromComparison
{
    public class AddProductToFavouritesFromComparisonViewComponent : ViewComponent
    {
        private readonly IProductsStorage productsStorage;
        private readonly IFavouritesStorage favouritesStorage;
        public AddProductToFavouritesFromComparisonViewComponent(IProductsStorage productsStorage, IFavouritesStorage favouritesStorage)
        {
            this.productsStorage = productsStorage;
            this.favouritesStorage = favouritesStorage;
        }
        public IViewComponentResult Invoke(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var item = favouritesStorage.TryGetByUserId(Constants.UserId);
            ViewBag.Comparison = item.Products;
            return View("AddProductToFavouritesFromComparison", product);
        }
    }
}