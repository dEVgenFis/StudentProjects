using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class PageNumberView
    {
        public List<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}