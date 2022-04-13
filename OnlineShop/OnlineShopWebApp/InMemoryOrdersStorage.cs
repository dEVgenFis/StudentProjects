using OnlineShopWebApp.Areas.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class InMemoryOrdersStorage : IOrdersStorage
    {
        private List<Order> orders = new List<Order>();
        public List<Order> GetAllOrders()
        {
            return orders;
        }
        public Order TryGetByOrderId(Guid orderId)
        {
            return orders.FirstOrDefault(order => order.Id == orderId);
        }
        public void AddUserOrder(string userId, Cart userCart, ClientContacts clientContacts, DeliveryAddress deliveryAddress, string clientComment)
        {
            var userOrder = new Order
            {
                UserId = userId,
                Cart = new Cart
                {
                    UserId = Constants.UserId,
                    Items = new List<CartItem>(userCart.Items)
                },
                ClientContacts = clientContacts,
                DeliveryAddress = deliveryAddress,
                Comment = clientComment,
                
            };
            orders.Add(userOrder);
            userCart.Items.Clear();
        }
        public void UpdateUserOrderStatus(Guid orderId, OrderStatuses newStatus)
        {
            var order = TryGetByOrderId(orderId);
            order.Status = newStatus;
        }
    }
}