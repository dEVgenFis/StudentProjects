using System.Collections.Generic;

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
                if (Products.Count != 0)
                {
                    decimal minCost = Products[0].Cost;
                    for (int i = 1; i < Products.Count; i++)
                    {
                        if (Products[i].Cost < minCost)
                        {
                            minCost = Products[i].Cost;
                        }
                    }
                    return minCost;
                }
                return default;
            }
        }
    }
}