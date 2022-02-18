using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Comparison
{
    public class ComparisonViewComponent : ViewComponent
    {
        private readonly IProductsComparisonStorage comparisonStorage;
        public ComparisonViewComponent(IProductsComparisonStorage comparisonStorage)
        {
            this.comparisonStorage = comparisonStorage;
        }
        public IViewComponentResult Invoke()
        {
            var item = comparisonStorage.TryGetByUserId(Constants.UserId);
            if (Constants.Theme == Theme.Light)
            {
                return View("Comparison", item.Amount);
            }
            return View("ComparisonDark", item.Amount);
        }
    }
}