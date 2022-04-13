using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class InMemoryFavouritesStorage : IFavouritesStorage
    {
        private List<Favourites> favourites = new List<Favourites>();
        public Favourites TryGetByUserId(string userId)
        {
            var userFavourites = favourites.FirstOrDefault(item => item.UserId == userId);
            if (userFavourites is null)
            {
                userFavourites = new Favourites {
                    UserId = userId,
                    Products = new List<Product>()
                };
                favourites.Add(userFavourites);
            }
            return userFavourites;
        }
        public void AddProduct(Product product, Favourites userFavourites)
        {
            if (!userFavourites.Products.Contains(product))
            {
                userFavourites.Products.Add(product);
            }
        }
        public void RemoveProduct(Product product, Favourites userFavourites)
        {
            userFavourites.Products.Remove(product);
        }
        public void Clear(Favourites userFavourites)
        {
            userFavourites.Products.Clear();
        }
    }
}