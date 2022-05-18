using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

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
        public IActionResult Index()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/home/index");
            productsStorage.ResetSearchWorkLocations();
            var сatalog = productsStorage.GetAllProducts()
                                         .UserSortingProducts(Constants.UserSortingProductsValue);
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            ViewBag.MinCost = productsStorage.MinCost;
            ViewBag.MaxCost = productsStorage.MaxCost;
            ViewBag.SortingProductsValueView = Constants.UserSortingProductsValueView;
            if (Constants.Theme == Theme.Light)
            {
                return View(сatalog);
            }
            return View("IndexDark", сatalog);
        }
        public IActionResult Filtering()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/home/filtering");
            var searchWord = productsStorage.UserSearchWord;
            var locations = productsStorage.SearchWorkLocations;
            var minCost = productsStorage.SearchMinCost;
            var maxCost = productsStorage.SearchMaxCost;
            var filteringCatalog = productsStorage.FilteringProducts(searchWord, locations, minCost, maxCost)
                                                  .UserSortingProducts(Constants.UserSortingProductsValue);
            ViewBag.SearchWord = searchWord;
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            ViewBag.FilteredLocations = productsStorage.SearchWorkLocations;
            ViewBag.MinCost = productsStorage.MinCost;
            ViewBag.SearchMinCost = productsStorage.SearchMinCost;
            ViewBag.MaxCost = productsStorage.MaxCost;
            ViewBag.SearchMaxCost = productsStorage.SearchMaxCost;
            ViewBag.SortingProductsValueView = Constants.UserSortingProductsValueView;
            if (Constants.Theme == Theme.Light)
            {
                return View(filteringCatalog);
            }
            return View("FilteringDark", filteringCatalog);
        }

        [HttpPost]
        public IActionResult Filtering(string searchWord, string[] locations, decimal minCost, decimal maxCost)
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/home/filtering");
            var filteringCatalog = productsStorage.FilteringProducts(searchWord, locations, minCost, maxCost)
                                                  .UserSortingProducts(Constants.UserSortingProductsValue);
            ViewBag.SearchWord = searchWord?.Trim();
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            ViewBag.FilteredLocations = productsStorage.SearchWorkLocations;
            ViewBag.MinCost = productsStorage.MinCost;
            ViewBag.SearchMinCost = productsStorage.SearchMinCost;
            ViewBag.MaxCost = productsStorage.MaxCost;
            ViewBag.SearchMaxCost = productsStorage.SearchMaxCost;
            ViewBag.SortingProductsValueView = Constants.UserSortingProductsValueView;
            if (Constants.Theme == Theme.Light)
            {
                return View(filteringCatalog);
            }
            return View("FilteringDark", filteringCatalog);
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