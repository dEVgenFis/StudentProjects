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
        void AddUserOrder(string userId, Cart userCart, ClientContacts clientContacts, DeliveryAddress deliveryAddress, string clientComment);
        void UpdateUserOrderStatus(Guid orderId, OrderStatuses newStatus);
    }
}