using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IProductsComparisonStorage
    {
        Comparison TryGetByUserId(string userId);
        void AddProduct(Product product, Comparison userProductsComparisonList);
        void RemoveProduct(Product product, Comparison userProductsComparisonList);
        void Clear(Comparison userProductsComparisonList);
    }
}