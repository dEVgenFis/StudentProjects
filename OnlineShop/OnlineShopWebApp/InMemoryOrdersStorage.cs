using OnlineShopWebApp.Areas.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

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
            foreach (var item in orders)
            {
                if (item.Id == orderId)
                {
                    return item;
                }
            }
            return null;
        }
        public void AddOrder(Order order)
        {
            orders.Add(order);
        }
        public void UpdateOrderStatus(Guid orderId, OrderStatuses newStatus)
        {
            var order = TryGetByOrderId(orderId);
            if (order == null)
            {
                return;
            }
            order.Status = newStatus;
        }
    }
}