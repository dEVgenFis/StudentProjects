using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public class InMemoryCartsStorage : ICartsStorage
    {
        private List<Cart> carts = new List<Cart>()
        {
            new Cart
            {
                UserId = Constants.UserId,
                Items = new List<CartItem>()
            }
        };
        public Cart TryGetByUserId(string userId)
        {
            foreach (var item in carts)
            {
                if (item.UserId == userId)
                {
                    return item;
                }
            }
            return null;
        }
        public void Add(Product product, string userId)
        {
            var cart = TryGetByUserId(userId);
            CartItem cartItem = null;
            foreach (var item in cart.Items)
            {
                if (item.Product == product)
                {
                    item.Amount += 1;
                    cartItem = item;
                    break;
                }
            }
            if (cartItem == null)
            {
                cart.Items.Add(new CartItem
                {
                    Product = product,
                    Amount = 1
                });
            }
        }
        public void DecreaseAmount(Guid productId, string userId)
        {
            var cart = TryGetByUserId(userId);
            CartItem cartItem = null;
            foreach (var item in cart.Items)
            {
                if (item.Product.Id == productId)
                {
                    item.Amount -= 1;
                    cartItem = item;
                    break;
                }
            }
            if (cartItem.Amount == 0)
            {
                cart.Items.Remove(cartItem);
            }
        }
        public void RemovePosition(Guid productId, string userId)
        {
            var cart = TryGetByUserId(userId);
            foreach (var item in cart.Items)
            {
                if (item.Product.Id == productId)
                {
                    cart.Items.Remove(item);
                    break;
                }
            }
        }
        public void Clear(string userId)
        {
            var cart = TryGetByUserId(userId);
            cart.Items.Clear();
        }
    }
}