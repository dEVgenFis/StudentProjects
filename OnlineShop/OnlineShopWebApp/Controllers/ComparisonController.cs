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
            var userProductsComparisonList = comparisonStorage.TryGetByUserId(Constants.UserId);
            ViewBag.MinCost = userProductsComparisonList.MinCost;
            if (Constants.Theme == Theme.Light)
            {
                return View(userProductsComparisonList.Products);
            }
            return View("IndexDark", userProductsComparisonList.Products);
        }
        public IActionResult AddProduct(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var userProductsComparisonList = comparisonStorage.TryGetByUserId(Constants.UserId);
            comparisonStorage.AddProduct(product, userProductsComparisonList);
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
        public IActionResult RemoveProduct(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            var userProductsComparisonList = comparisonStorage.TryGetByUserId(Constants.UserId);
            comparisonStorage.RemoveProduct(product, userProductsComparisonList);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            var userProductsComparisonList = comparisonStorage.TryGetByUserId(Constants.UserId);
            comparisonStorage.Clear(userProductsComparisonList);
            return RedirectToAction("Index");
        }
    }
}