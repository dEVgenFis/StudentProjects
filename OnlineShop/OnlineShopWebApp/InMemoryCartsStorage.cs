using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class InMemoryCartsStorage : ICartsStorage
    {
        private List<Cart> carts = new List<Cart>();
        public Cart TryGetByUserId(string userId)
        {
            var userCart = carts.FirstOrDefault(cart => cart.UserId == userId);
            if (userCart is null)
            {
                userCart = new Cart {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                carts.Add(userCart);
            }
            return userCart;
        }
        public void Add(Product product, Cart userCart)
        {
            var cartItem = userCart.Items.FirstOrDefault(item => item.Product == product);
            if (cartItem == null)
            {
                userCart.Items.Add(new CartItem {
                    Product = product,
                    Amount = 1
                });
            }
            else
            {
                cartItem.Amount++;
            }
        }
        public void DecreaseAmount(Product product, Cart userCart)
        {
            var cartItem = userCart.Items.FirstOrDefault(item => item.Product == product);
            cartItem.Amount--;
            if (cartItem.Amount == 0)
            {
                userCart.Items.Remove(cartItem);
            }
        }
        public void RemovePosition(Product product, Cart userCart)
        {
            var cartItem = userCart.Items.FirstOrDefault(item => item.Product == product);
            userCart.Items.Remove(cartItem);
        }
        public void Clear(Cart userCart)
        {
            userCart.Items.Clear();
        }
    }
}