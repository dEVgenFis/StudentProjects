using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsStorage productsStorage;
        public ProductController(IProductsStorage productsStorage)
        {
            this.productsStorage = productsStorage;
        }
        public IActionResult Index(Guid productId)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/product/index?productId={productId}");
            var product = productsStorage.TryGetProductById(productId);
            if (product is null) { return NotFound(); }
            ViewBag.ProductWorkLocations = productsStorage.GetProductWorkLocations(product);
            if (Constants.Theme == Theme.Light)
            {
                return View(product);
            }
            return View("IndexDark", product);
        }
    }
}