using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace OnlineShopWebApp.Views.Shared.Components.AddProductToCartFromComparison
{
    public class AddProductToCartFromComparisonViewComponent : ViewComponent
    {
        private readonly IProductsStorage productsStorage;
        private readonly ICartsStorage cartsStorage;
        public AddProductToCartFromComparisonViewComponent(IProductsStorage productsStorage, ICartsStorage cartsStorage)
        {
            this.productsStorage = productsStorage;
            this.cartsStorage = cartsStorage;
        }
        public IViewComponentResult Invoke(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var cart = cartsStorage.TryGetByUserId(Constants.UserId);
            ViewBag.ExistsInCart = cart.Items.FirstOrDefault(cartItem => cartItem.Product.Equals(product)) is null;
            return View("AddProductToCartFromComparison", product);
        }
    }
}