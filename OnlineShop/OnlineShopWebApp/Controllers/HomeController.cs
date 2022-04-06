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
            productsStorage.ResetWorkLocations();
            var сatalog = productsStorage.GetAllProducts();
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            ViewBag.MinCost = productsStorage.MinCost;
            ViewBag.MaxCost = productsStorage.MaxCost;
            if (Constants.Theme == Theme.Light)
            {
                return View(сatalog);
            }
            return View("IndexDark", сatalog);
        }
        public IActionResult Filtering()
        {
            Constants.ReturnPathToCurrentPage = string.Intern("~/home/filtering");
            var searchWord = productsStorage.SearchWord;
            var locations = productsStorage.Locations;
            var minCost = productsStorage.SearchMinCost;
            var maxCost = productsStorage.SearchMaxCost;
            var filteringCatalog = productsStorage.FilteringProducts(searchWord, locations, minCost, maxCost);
            ViewBag.SearchWord = searchWord;
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            ViewBag.FilteredLocations = productsStorage.Locations;
            ViewBag.MinCost = productsStorage.MinCost;
            ViewBag.SearchMinCost = productsStorage.SearchMinCost;
            ViewBag.MaxCost = productsStorage.MaxCost;
            ViewBag.SearchMaxCost = productsStorage.SearchMaxCost;
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
            var filteringCatalog = productsStorage.FilteringProducts(searchWord, locations, minCost, maxCost);
            ViewBag.SearchWord = searchWord;
            ViewBag.AllWorkLocations = locationsStorage.GetAllWorkLocations();
            ViewBag.FilteredLocations = productsStorage.Locations;
            ViewBag.MinCost = productsStorage.MinCost;
            ViewBag.SearchMinCost = productsStorage.SearchMinCost;
            ViewBag.MaxCost = productsStorage.MaxCost;
            ViewBag.SearchMaxCost = productsStorage.SearchMaxCost;
            if (Constants.Theme == Theme.Light)
            {
                return View(filteringCatalog);
            }
            return View("FilteringDark", filteringCatalog);
        }

        public IActionResult RemoveSearchWord()
        {
            productsStorage.ResetSearchWord();
            return RedirectToAction("Filtering");
        }
    }
}