using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public static class Constants
    {
        public static string UserId = "UserId";
        public static int SortingProductsValue { get; set; }
        public static string SortingProductsValueView { get; set; }
        public static Theme Theme { get; set; }
        public static string ReturnPathToCurrentPage { get; set; }
        public static List<Product> SortingProducts(this List<Product> products, int sortingValue)
        {
            SortingProductsValue = sortingValue;
            if (sortingValue < 0)
            {
                SortingProductsValueView = string.Intern("убыванию цены");
                return products.OrderByDescending(product => product.Cost).ToList();
            }
            else
            {
                SortingProductsValueView = string.Intern("возрастанию цены");
                return products.OrderBy(product => product.Cost).ToList();
            }
        }
    }
}