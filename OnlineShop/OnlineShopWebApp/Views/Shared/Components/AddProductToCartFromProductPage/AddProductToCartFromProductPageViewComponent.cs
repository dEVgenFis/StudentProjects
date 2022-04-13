using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace OnlineShopWebApp.Views.Shared.Components.AddProductToCartFromProductPage
{
    public class AddProductToCartFromProductPageViewComponent : ViewComponent
    {
        private readonly IProductsStorage productsStorage;
        private readonly ICartsStorage cartsStorage;
        public AddProductToCartFromProductPageViewComponent(IProductsStorage productsStorage, ICartsStorage cartsStorage)
        {
            this.productsStorage = productsStorage;
            this.cartsStorage = cartsStorage;
        }
        public IViewComponentResult Invoke(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var cart = cartsStorage.TryGetByUserId(Constants.UserId);
            ViewBag.ExistsInCart = cart.Items.FirstOrDefault(cartItem => cartItem.Product == product) is null;
            return View("AddProductToCartFromProductPageDark", product);
        }
    }
}