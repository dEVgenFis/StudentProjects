using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class InMemoryProductsStorage : IProductsStorage
    {
        private List<Product> productCatalog = new List<Product>
        {
            new Product("Иван И.", 8000, new List<WorkLocation> {new WorkLocation("Самара"), new WorkLocation("Кинель"), new WorkLocation("Новокуйбышевск")}, "Сертификат об окончании курсов подготовки кадров по специальности \"Сварочные работы\" № ... от ...", "Сварочный аппарат модели ... № ...", "...", "/images/Ivan.jpg"),
            new Product("Глеб Г.", 3000, new List<WorkLocation> {new WorkLocation("Кинель") }, "", "", "", "/images/Gleb.jpg"),
            new Product("Петр П.", 5500, new List<WorkLocation> { new WorkLocation("Тольятти") }, "", "Сварочный аппарат модели ... № ...", "", "/images/Petr.jpg"),
            new Product("Ирина И.", 3000, new List<WorkLocation> { new WorkLocation("Самара"), new WorkLocation("Тольятти") }, "", "", "", "/images/Irina.jpg"),
            new Product("Кирилл К.", 1100, new List<WorkLocation>(), "", "Сварочный аппарат модели ... № ...", "", "/images/Kirill.jpg")
        };
        private List<Product> filteredProductCatalog = new List<Product>();
        public string SearchWord { get; set; }
        public decimal MinCost
        {
            get
            {
                return productCatalog.Count.Equals(0) ? default : productCatalog.Min(product => product.Cost);
            }
        }
        public decimal SearchMinCost { get; set; }
        public decimal MaxCost
        {
            get
            {
                return productCatalog.Count.Equals(0) ? default : productCatalog.Max(product => product.Cost);
            }
        }
        public decimal SearchMaxCost { get; set; }
        public string[] SearchWorkLocations { get; set; }
        public List<Product> GetAllProducts()
        {
            return productCatalog;
        }
        public Product TryGetProductById(Guid productId)
        {
            return productCatalog.FirstOrDefault(product => product.Id == productId);
        }
        public List<Product> FilteringProducts(string searchWord, string[] locations, decimal minCost, decimal maxCost)
        {
            filteredProductCatalog.Clear();
            SearchWord = searchWord?.ToLower().Trim();
            SearchWorkLocations = locations;
            if (minCost > maxCost)
            {
                SearchMinCost = MinCost;
            }
            else
            {
                SearchMinCost = minCost;
            }
            SearchMaxCost = maxCost;
            foreach (var product in productCatalog)
            {
                if (product.Cost >= SearchMinCost && product.Cost <= SearchMaxCost)
                {
                    if (SearchWorkLocations.Length == 0)
                    {
                        if (string.IsNullOrEmpty(SearchWord))
                        {
                            filteredProductCatalog.Add(product);
                        }
                        else
                        {
                            if (product.Name.ToLower().Contains(SearchWord))
                            {
                                filteredProductCatalog.Add(product);
                            }
                        }
                    }
                    else
                    {
                        foreach (var location in SearchWorkLocations)
                        {
                            if (product.Locations.Contains(new WorkLocation(location)))
                            {
                                if (string.IsNullOrEmpty(SearchWord))
                                {
                                    filteredProductCatalog.Add(product);
                                    break;
                                }
                                else
                                {
                                    if (product.Name.ToLower().Contains(SearchWord))
                                    {
                                        filteredProductCatalog.Add(product);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return filteredProductCatalog;
        }
        public void AddProduct(Product product)
        {
            productCatalog.Add(product);
        }
        public void UpdateProduct(Product product)
        {
            var item = productCatalog.First(item => item.Id == product.Id);
            item.Name = product.Name;
            item.Cost = product.Cost;
            item.Locations = product.Locations;
            item.Documents = product.Documents;
            item.Equipment = product.Equipment;
            item.Description = product.Description;
        }
        public void RemoveProduct(Product product)
        {
            productCatalog.Remove(product);
        }
        public string GetProductWorkLocations(Product product)
        {
            if (product.Locations.Count.Equals(0))
            {
                return "Мастер не указал рабочие города.";
            }
            var workLocations = string.Intern(string.Empty);
            for (int i = 0; i < product.Locations.Count - 1; i++)
            {
                workLocations += product.Locations[i] + ", ";
            }
            workLocations += product.Locations[^1];
            return workLocations;
        }
        public void AddProductWorkLocations(Product product, string[] locations)
        {
            if (locations.Length.Equals(0))
            {
                product.Locations = new List<WorkLocation>();
            }
            else
            {
                foreach (var location in locations)
                {
                    product.Locations.Add(new WorkLocation(location));
                }
            }
        }
        public void ResetSearchWord()
        {
            if (string.IsNullOrEmpty(SearchWord)) { return; }
            SearchWord = default;
        }
        
        public void ResetSearchWorkLocations()
        {
            if (SearchWorkLocations is null) { return; }
            Array.Clear(SearchWorkLocations, 0, SearchWorkLocations.Length);
        }
        
    }
}