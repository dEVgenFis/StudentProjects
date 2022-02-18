using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class Favourites
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
    }
}