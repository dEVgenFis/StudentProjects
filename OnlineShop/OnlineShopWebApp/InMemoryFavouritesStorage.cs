using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public class InMemoryFavouritesStorage : IFavouritesStorage
    {
        private List<Favourites> favourites = new List<Favourites>()
        {
            new Favourites
            {
                UserId = Constants.UserId,
                Products = new List<Product>()
            }
        };
        public Favourites TryGetByUserId(string userId)
        {
            foreach (var item in favourites)
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
            if (!item.Products.Contains(product))
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