using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IFavouritesStorage
    {
        Favourites TryGetByUserId(string userId);
        void AddProduct(Product product, string userId);
        void RemoveProduct(Product product, string userId);
        void Clear(string userId);
    }
}