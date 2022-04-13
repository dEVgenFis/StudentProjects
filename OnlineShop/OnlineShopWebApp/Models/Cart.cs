using System.Collections.Generic;
using System.Linq;

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
                return Items.Sum(item => item.Cost);
            }
        }
        public int Amount
        {
            get
            {
                return Items.Sum(item => item.Amount);
            }
        }
    }
}