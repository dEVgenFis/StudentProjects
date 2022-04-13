using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class Comparison
    {
        public string UserId { get; set; }
        public List<Product> Products { get; set; }
        public int Amount
        {
            get
            {
                return Products.Count;
            }
        }
        public decimal MinCost
        {
            get
            {
                return Products.Count != 0 ? Products.Min(product => product.Cost) : default;
            }
        }
    }
}