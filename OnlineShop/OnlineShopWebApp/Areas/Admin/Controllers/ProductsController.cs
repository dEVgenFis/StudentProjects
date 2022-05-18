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
            productsStorage.ResetAdminSearchWord();
            var сatalog = productsStorage.GetAllProducts()
                                         .AdminSortingProducts(Constants.AdminSortingProductsValue);
            ViewBag.SearchWord = productsStorage.AdminSearchWord;
            ViewBag.SortingProductsValueView = Constants.AdminSortingProductsValueView;
            ViewBag.Path = Constants.ReturnPathToCurrentPage;
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View(сatalog);
            }
            return View("IndexDark", сatalog);
        }
        public IActionResult Search()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/products/search");
            var searchWord = productsStorage.AdminSearchWord;
            var сatalog = productsStorage.SearchProductByName(searchWord)
                                         .AdminSortingProducts(Constants.AdminSortingProductsValue);
            ViewBag.SearchWord = searchWord;
            ViewBag.SortingProductsValueView = Constants.AdminSortingProductsValueView;
            ViewBag.Path = Constants.ReturnPathToCurrentPage;
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View("Index", сatalog);
            }
            return View("IndexDark", сatalog);
        }

        [HttpPost]
        public IActionResult Search(string searchWord)
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/products/search");
            var сatalog = productsStorage.SearchProductByName(searchWord)
                                         .AdminSortingProducts(Constants.AdminSortingProductsValue);
            ViewBag.SearchWord = searchWord?.Trim();
            ViewBag.SortingProductsValueView = Constants.AdminSortingProductsValueView;
            ViewBag.Path = Constants.ReturnPathToCurrentPage;
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View("Index", сatalog);
            }
            return View("IndexDark", сatalog);
        }

        public IActionResult Add()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/admin/products/add");
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            if (Constants.Theme.Equals(Theme.Light))
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
            productsStorage.AddProductWorkLocations(product, locations);
            productsStorage.AddProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid productId)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/admin/products/edit?productId={productId}");
            var product = productsStorage.TryGetProductById(productId);
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            if (Constants.Theme.Equals(Theme.Light))
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
            productsStorage.AddProductWorkLocations(product, locations);
            productsStorage.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            var product = productsStorage.TryGetProductById(productId);
            productsStorage.RemoveProduct(product);
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
        public IActionResult Sorting(int sortingValue)
        {
            Constants.AdminSortingProductsValue = sortingValue;
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
    }
}