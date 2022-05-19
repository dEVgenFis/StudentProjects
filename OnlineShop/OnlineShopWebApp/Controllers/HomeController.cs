using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Linq;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IWorkLocationsStorage locationsStorage;
        public HomeController(IProductsStorage productsStorage, IWorkLocationsStorage locationsStorage)
        {
            this.productsStorage = productsStorage;
            this.locationsStorage = locationsStorage;
        }
        public IActionResult Index(int page = 1)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/home/index?page={page}");
            productsStorage.ResetSearchWorkLocations();
            int pageSize = 5;
            var сatalog = productsStorage.GetAllProducts()
                                         .UserSortingProducts(Constants.UserSortingProductsValue)
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
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            ViewBag.MinCost = productsStorage.MinCost;
            ViewBag.MaxCost = productsStorage.MaxCost;
            ViewBag.SortingProductsValueView = Constants.UserSortingProductsValueView;
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View(pnv);
            }
            return View("IndexDark", pnv);
        }
        public IActionResult Filtering(int page = 1)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/home/filtering?page={page}");
            int pageSize = 5;
            var searchWord = productsStorage.UserSearchWord;
            var locations = productsStorage.SearchWorkLocations;
            var minCost = productsStorage.SearchMinCost;
            var maxCost = productsStorage.SearchMaxCost;
            var filteringCatalog = productsStorage.FilteringProducts(searchWord, locations, minCost, maxCost)
                                                  .UserSortingProducts(Constants.UserSortingProductsValue)
                                                  .Skip((page - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToList();
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = productsStorage.FilteringProducts(searchWord, locations, minCost, maxCost).Count()
            };
            var pnv = new PageNumberView
            {
                Products = filteringCatalog,
                PageInfo = pageInfo
            };
            ViewBag.SearchWord = searchWord;
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            ViewBag.FilteredLocations = productsStorage.SearchWorkLocations;
            ViewBag.MinCost = productsStorage.MinCost;
            ViewBag.SearchMinCost = productsStorage.SearchMinCost;
            ViewBag.MaxCost = productsStorage.MaxCost;
            ViewBag.SearchMaxCost = productsStorage.SearchMaxCost;
            ViewBag.SortingProductsValueView = Constants.UserSortingProductsValueView;
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View(pnv);
            }
            return View("FilteringDark", pnv);
        }

        [HttpPost]
        public IActionResult Filtering(string searchWord, string[] locations, decimal minCost, decimal maxCost, int page = 1)
        {
            Constants.ReturnPathToCurrentPage = string.Intern($"~/home/filtering?page={page}");
            int pageSize = 5;
            var filteringCatalog = productsStorage.FilteringProducts(searchWord, locations, minCost, maxCost)
                                                  .UserSortingProducts(Constants.UserSortingProductsValue)
                                                  .Skip((page - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToList();
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = productsStorage.FilteringProducts(searchWord, locations, minCost, maxCost).Count()
            };
            var pnv = new PageNumberView
            {
                Products = filteringCatalog,
                PageInfo = pageInfo
            };
            ViewBag.SearchWord = searchWord?.Trim();
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            ViewBag.FilteredLocations = productsStorage.SearchWorkLocations;
            ViewBag.MinCost = productsStorage.MinCost;
            ViewBag.SearchMinCost = productsStorage.SearchMinCost;
            ViewBag.MaxCost = productsStorage.MaxCost;
            ViewBag.SearchMaxCost = productsStorage.SearchMaxCost;
            ViewBag.SortingProductsValueView = Constants.UserSortingProductsValueView;
            if (Constants.Theme.Equals(Theme.Light))
            {
                return View(pnv);
            }
            return View("FilteringDark", pnv);
        }

        public IActionResult RemoveSearchWord()
        {
            productsStorage.ResetUserSearchWord();
            return RedirectToAction("Filtering");
        }
        public IActionResult Sorting(int sortingValue)
        {
            Constants.UserSortingProductsValue = sortingValue;
            return Redirect(Constants.ReturnPathToCurrentPage);
        }
    }
}