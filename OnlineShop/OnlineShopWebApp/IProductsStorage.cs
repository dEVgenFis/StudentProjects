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
        string[] Locations { get; set; }
        List<Product> GetAllProducts();
        List<Product> FilteringProducts(string searchWord, string[] locations, decimal minCost, decimal maxCost);
        void ResetSearchWord();
        string GetProductWorkLocations(Product product);
        void AddWorkLocations(Product product, string[] locations);
        void ResetWorkLocations();
        Product TryGetProductById(Guid productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void RemoveProduct(Guid productId);
    }
}