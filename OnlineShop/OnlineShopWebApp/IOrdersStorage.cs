using OnlineShopWebApp.Areas.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public interface IOrdersStorage
    {
        List<Order> GetAllOrders();
        Order TryGetByOrderId(Guid orderId);
        void AddOrder(Order order);
        void UpdateOrderStatus(Guid orderId, OrderStatuses newStatus);
    }
}