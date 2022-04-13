using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public interface IProductsStorage
    {
        string SearchWord { get; set; }
        decimal MinCost { get; }
        decimal SearchMinCost { get; set; }
        decimal MaxCost { get; }
        decimal SearchMaxCost { get; set; }
        string[] SearchWorkLocations { get; set; }
        List<Product> GetAllProducts();
        Product TryGetProductById(Guid productId);
        List<Product> FilteringProducts(string searchWord, string[] locations, decimal minCost, decimal maxCost);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void RemoveProduct(Product product);
        string GetProductWorkLocations(Product product);
        void AddProductWorkLocations(Product product, string[] locations);
        void ResetSearchWord();
        void ResetSearchWorkLocations();
        
    }
}