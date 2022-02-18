using Microsoft.AspNetCore.Mvc;
using System;

namespace OnlineShopWebApp.Views.Shared.Components.AddProductToComparison
{
    public class AddProductToComparisonViewComponent : ViewComponent
    {
        private readonly IProductsStorage productsStorage;
        private readonly IProductsComparisonStorage comparisonStorage;
        public AddProductToComparisonViewComponent(IProductsStorage productsStorage, IProductsComparisonStorage comparisonStorage)
        {
            this.productsStorage = productsStorage;
            this.comparisonStorage = comparisonStorage;
        }
        public IViewComponentResult Invoke(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var item = comparisonStorage.TryGetByUserId(Constants.UserId);
            ViewBag.Comparison = item.Products;
            return View("AddProductToComparison", product);
        }
    }
}