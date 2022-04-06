using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Areas.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IWorkLocationsStorage locationsStorage;
        public ProductsController(IProductsStorage productsStorage, IWorkLocationsStorage locationsStorage)
        {
            this.productsStorage = productsStorage;
            this.locationsStorage = locationsStorage;
        }
        public IActionResult Index()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/products/index");
            var сatalog = productsStorage.GetAllProducts();
            if (Constants.Theme == Theme.Light)
            {
                return View(сatalog);
            }
            return View("IndexDark", сatalog);
        }
        public IActionResult Add()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/products/add");
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            if (Constants.Theme == Theme.Light)
            {
                return View();
            }
            return View("AddDark");
        }

        [HttpPost]
        public IActionResult Add(Product product, string[] locations)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            productsStorage.AddWorkLocations(product, locations);
            productsStorage.AddProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid productId)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/admin/products/edit?productId={productId}");
            var product = productsStorage.TryGetProductById(productId);
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            if (Constants.Theme == Theme.Light)
            {
                return View(product);
            }
            return View("EditDark", product);
        }

        [HttpPost]
        public IActionResult Edit(Product product, string[] locations)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            productsStorage.AddWorkLocations(product, locations);
            productsStorage.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            productsStorage.RemoveProduct(productId);
            return RedirectToAction("Index");
        }
    }
}