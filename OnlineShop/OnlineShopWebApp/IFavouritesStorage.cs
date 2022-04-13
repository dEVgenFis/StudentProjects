using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IFavouritesStorage
    {
        Favourites TryGetByUserId(string userId);
        void AddProduct(Product product, Favourites userFavourites);
        void RemoveProduct(Product product, Favourites userFavourites);
        void Clear(Favourites userFavourites);
    }
}