using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public class InMemoryProductsComparisonStorage : IProductsComparisonStorage
    {
        private List<Comparison> comparisonCatalog = new List<Comparison>
        {
            new Comparison
            {
                UserId = Constants.UserId,
                Products = new List<Product>()
            }
        };
        public Comparison TryGetByUserId(string userId)
        {
            foreach (var item in comparisonCatalog)
            {
                if (item.UserId == userId)
                {
                    return item;
                }
            }
            return null;
        }
        public void AddProduct(Product product, string userId)
        {
            var item = TryGetByUserId(userId);
            if (!item.Products.Contains(product) && item.Products.Count < 4)
            {
                item.Products.Add(product);
            }
        }
        public void RemoveProduct(Product product, string userId)
        {
            var item = TryGetByUserId(userId);
            item.Products.Remove(product);
        }
        public void Clear(string userId)
        {
            var item = TryGetByUserId(userId);
            item.Products.Clear();
        }
    }
}