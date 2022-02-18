using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp
{
    public interface ICartsStorage
    {
        Cart TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void DecreaseAmount(Guid productId, string userId);
        void RemovePosition(Guid productId, string userId);
        void Clear(string userId);
    }
}