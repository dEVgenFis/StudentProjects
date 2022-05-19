using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System;
using System.Linq;

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
        public IActionResult Index(int page = 1)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/admin/products/index?page={page}");
            productsStorage.ResetAdminSearchWord();
            int pageSize = 5;
            var сatalog = productsStorage.GetAllProducts()
                                         .AdminSortingProducts(Constants.AdminSortingProductsValue)
                                         .Skip((page - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToList();
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = productsStorage.GetAllProducts().Count()
            };
            var pnv = new PageNumberView
            {
                Products = сatalog,
                PageInfo = pageInfo
            };
            ViewBag.SearchWord = productsStorage.AdminSearchWord;
            ViewBag.SortingProductsValueView = Constants.AdminSortingProductsValueView;
            ViewBag.Catalog = productsStorage.GetAllProducts()
                                             .AdminSortingProducts(Constants.AdminSortingProductsValue)
                                             .ToList();
            ViewBag.Path = Constants.ReturnPathToCurrentPage;
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View(pnv);
            }
            return View("IndexDark", pnv);
        }
        public IActionResult Search(int page = 1)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/admin/products/search?page={page}");
            int pageSize = 5;
            var searchWord = productsStorage.AdminSearchWord;
            var сatalog = productsStorage.SearchProductByName(searchWord)
                                         .AdminSortingProducts(Constants.AdminSortingProductsValue)
                                         .Skip((page - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToList();
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = productsStorage.SearchProductByName(searchWord).Count()
            };
            var pnv = new PageNumberView
            {
                Products = сatalog,
                PageInfo = pageInfo
            };
            ViewBag.SearchWord = searchWord;
            ViewBag.SortingProductsValueView = Constants.AdminSortingProductsValueView;
            ViewBag.Catalog = productsStorage.SearchProductByName(searchWord)
                                             .AdminSortingProducts(Constants.AdminSortingProductsValue)
                                             .ToList();
            ViewBag.Path = Constants.ReturnPathToCurrentPage;
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View("Index", pnv);
            }
            return View("IndexDark", pnv);
        }

        [HttpPost]
        public IActionResult Search(string searchWord, int page = 1)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/admin/products/search?page={page}");
            int pageSize = 5;
            var сatalog = productsStorage.SearchProductByName(searchWord)
                                         .AdminSortingProducts(Constants.AdminSortingProductsValue)
                                         .Skip((page - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToList();
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = productsStorage.SearchProductByName(searchWord).Count()
            };
            var pnv = new PageNumberView
            {
                Products = сatalog,
                PageInfo = pageInfo
            };
            ViewBag.SearchWord = searchWord?.Trim();
            ViewBag.SortingProductsValueView = Constants.AdminSortingProductsValueView;
            ViewBag.Catalog = productsStorage.SearchProductByName(searchWord)
                                             .AdminSortingProducts(Constants.AdminSortingProductsValue)
                                             .ToList();
            ViewBag.Path = Constants.ReturnPathToCurrentPage;
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View("Index", pnv);
            }
            return View("IndexDark", pnv);
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