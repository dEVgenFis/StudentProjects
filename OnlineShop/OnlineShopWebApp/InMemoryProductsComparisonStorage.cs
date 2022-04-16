using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class InMemoryProductsComparisonStorage : IProductsComparisonStorage
    {
        private readonly List<Comparison> productsComparisonCatalog = new List<Comparison>();
        public Comparison TryGetByUserId(string userId)
        {
            var userProductsComparisonList = productsComparisonCatalog.FirstOrDefault(item => item.UserId == userId);
            if (userProductsComparisonList is null)
            {
                userProductsComparisonList = new Comparison
                {
                    UserId = userId,
                    Products = new List<Product>()
                };
                productsComparisonCatalog.Add(userProductsComparisonList);
            }
            return userProductsComparisonList;
        }
        public void AddProduct(Product product, Comparison userProductsComparisonList)
        {
            if (!userProductsComparisonList.Products.Contains(product) && userProductsComparisonList.Products.Count < 4)
            {
                userProductsComparisonList.Products.Add(product);
            }
        }
        public void RemoveProduct(Product product, Comparison userProductsComparisonList)
        {
            userProductsComparisonList.Products.Remove(product);
        }
        public void Clear(Comparison userProductsComparisonList)
        {
            userProductsComparisonList.Products.Clear();
        }
    }
}