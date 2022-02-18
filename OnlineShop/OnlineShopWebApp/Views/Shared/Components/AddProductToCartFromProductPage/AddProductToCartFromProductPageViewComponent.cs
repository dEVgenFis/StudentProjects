using Microsoft.AspNetCore.Mvc;
using System;

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
            ViewBag.ExistsInCart = false;
            foreach (var item in cart.Items)
            {
                if (item.Product == product)
                {
                    ViewBag.ExistsInCart = true;
                    break;
                }
            }
            return View("AddProductToCartFromProductPageDark", product);
        }
    }
}