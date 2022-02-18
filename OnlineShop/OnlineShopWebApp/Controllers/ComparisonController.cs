using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class ComparisonController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IProductsComparisonStorage comparisonStorage;
        public ComparisonController(IProductsStorage productsStorage, IProductsComparisonStorage comparisonStorage)
        {
            this.productsStorage = productsStorage;
            this.comparisonStorage = comparisonStorage;
        }
        public IActionResult Index()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/comparison/index");
            var item = comparisonStorage.TryGetByUserId(Constants.UserId);
            ViewBag.MinCost = item.MinCost;
            if (Constants.Theme == Theme.Light)
            {
                return View(item.Products);
            }
            return View("indexdark", item.Products);
        }
        public IActionResult AddProduct(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            comparisonStorage.AddProduct(product, Constants.UserId);
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
        public IActionResult RemoveProduct(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            comparisonStorage.RemoveProduct(product, Constants.UserId);
            return RedirectToAction("index");
        }
        public IActionResult Clear()
        {
            comparisonStorage.Clear(Constants.UserId);
            return RedirectToAction("index");
        }
    }
}