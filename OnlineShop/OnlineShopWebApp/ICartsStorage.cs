using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface ICartsStorage
    {
        Cart TryGetByUserId(string userId);
        void Add(Product product, Cart userCart);
        void DecreaseAmount(Product product, Cart userCart);
        void RemovePosition(Product product, Cart userCart);
        void Clear(Cart userCart);
    }
}