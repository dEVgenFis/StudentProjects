using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public static class Constants
    {
        public static string UserId = "UserId";
        public static int AdminSortingProductsValue { get; set; } = 1;
        public static string AdminSortingProductsValueView { get; set; }
        public static int UserSortingProductsValue { get; set; } = 1;
        public static string UserSortingProductsValueView { get; set; }
        
        public static Theme Theme { get; set; }
        public static string ReturnPathToCurrentPage { get; set; }
        public static List<Product> AdminSortingProducts(this List<Product> products, int sortingValue)
        {
            AdminSortingProductsValue = sortingValue;
            if (sortingValue < 0)
            {
                AdminSortingProductsValueView = string.Intern("убыванию цены");
                return products.OrderByDescending(product => product.Cost)
                               .ToList();
            }
            else
            {
                AdminSortingProductsValueView = string.Intern("возрастанию цены");
                return products.OrderBy(product => product.Cost)
                               .ToList();
            }
        }
        public static List<Product> UserSortingProducts(this List<Product> products, int sortingValue)
        {
            UserSortingProductsValue = sortingValue;
            if (sortingValue < 0)
            {
                UserSortingProductsValueView = string.Intern("убыванию цены");
                return products.OrderByDescending(product => product.Cost)
                               .ToList();
            }
            else
            {
                UserSortingProductsValueView = string.Intern("возрастанию цены");
                return products.OrderBy(product => product.Cost)
                               .ToList();
            }
        }
    }
}