using System;

namespace OnlineShopWebApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ClientContacts ClientContacts { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public string Comment { get; set; }
        public Cart Cart { get; set; }
        public string CreationData { get; set; }
        public string CreationTime { get; set; }
        public OrderStatuses Status { get; set; }
        public Order()
        {
            Id = Guid.NewGuid();
            CreationData = DateTime.Now.ToShortDateString();
            CreationTime = DateTime.Now.ToShortTimeString();
            Status = 0;
        }
    }
}