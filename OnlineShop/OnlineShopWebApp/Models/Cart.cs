using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class Cart
    {
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal Cost
        {
            get
            {
                decimal cost = 0;
                foreach (var item in Items)
                {
                    cost += item.Cost;
                }
                return cost;
            }
        }
        public int Amount
        {
            get
            {
                int amount = 0;
                foreach (var item in Items)
                {
                    amount += item.Amount;
                }
                return amount;
            }
        }
    }
}