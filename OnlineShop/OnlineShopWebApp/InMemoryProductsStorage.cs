using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public class InMemoryProductsStorage : IProductsStorage
    {
        private List<Product> catalog { get; } = new List<Product>
        {
            new Product("Иван И.", 8000, new List<WorkLocation> {new WorkLocation("Самара"), new WorkLocation("Кинель"), new WorkLocation("Новокуйбышевск")}, "Сертификат об окончании курсов подготовки кадров по специальности \"Сварочные работы\" № ... от ...", "Сварочный аппарат модели ... № ...", "...", "/images/Ivan.jpg"),
            new Product("Глеб Г.", 3000, new List<WorkLocation> {new WorkLocation("Кинель") }, "", "", "", "/images/Gleb.jpg"),
            new Product("Петр П.", 5500, new List<WorkLocation> {new WorkLocation("Тольятти")}, "", "Сварочный аппарат модели ... № ...", "", "/images/Petr.jpg"),
            new Product("Ирина И.", 3000, new List<WorkLocation> {new WorkLocation ("Самара"), new WorkLocation("Тольятти") }, "", "", "", "/images/Irina.jpg"),
            new Product("Кирилл К.", 1100, new List<WorkLocation>(), "", "Сварочный аппарат модели ... № ...", "", "/images/Kirill.jpg")
        };
        private List<Product> filteringCatalog = new List<Product>();
        public string SearchWord { get; set; }
        public decimal MinCost
        {
            get
            {
                if (catalog.Count == 0) return default;
                decimal minCost = catalog[0].Cost;
                for (int i = 1; i < catalog.Count; i++)
                {
                    if (catalog[i].Cost < minCost)
                    {
                        minCost = catalog[i].Cost;
                    }
                }
                return minCost;
            }
        }
        public decimal SearchMinCost { get; set; }
        public decimal MaxCost
        {
            get
            {
                if (catalog.Count == 0) return default;
                decimal maxCost = catalog[^1].Cost;
                for (int i = catalog.Count - 2; i >= 0; i--)
                {
                    if (catalog[i].Cost > maxCost)
                    {
                        maxCost = catalog[i].Cost;
                    }
                }
                return maxCost;
            }
        }
        public decimal SearchMaxCost { get; set; }
        public string[] Locations { get; set; }
        public List<Product> GetAllProducts()
        {
            return catalog;
        }
        public List<Product> FilteringProducts(string searchWord, string[] locations, decimal minCost, decimal maxCost)
        {
            filteringCatalog.Clear();
            SearchWord = searchWord;
            Locations = locations;
            if (minCost > maxCost)
            {
                SearchMinCost = MinCost;
            }
            else
            {
                SearchMinCost = minCost;
            }
            SearchMaxCost = maxCost;
            foreach (var product in catalog)
            {
                if (product.Cost >= SearchMinCost && product.Cost <= SearchMaxCost)
                {
                    if (Locations.Length == 0)
                    {
                        if (string.IsNullOrEmpty(SearchWord))
                        {
                            filteringCatalog.Add(product);
                        }
                        else
                        {
                            if (product.Name.ToLower().Contains(SearchWord.ToLower()))
                            {
                                filteringCatalog.Add(product);
                            }
                        }
                    }
                    else
                    {
                        foreach (var location in Locations)
                        {
                            if (product.Locations.Contains(new WorkLocation(location)))
                            {
                                if (string.IsNullOrEmpty(SearchWord))
                                {
                                    filteringCatalog.Add(product);
                                    break;
                                }
                                else
                                {
                                    if (product.Name.ToLower().Contains(SearchWord.ToLower()))
                                    {
                                        filteringCatalog.Add(product);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return filteringCatalog;
        }
        public void ResetSearchWord()
        {
            if (string.IsNullOrEmpty(SearchWord)) { return; }
            SearchWord = string.Empty;
        }
        public string GetProductWorkLocations(Product product)
        {
            if (product.Locations.Count == 0)
            {
                return "Мастер не указал рабочие города.";
            }
            var workLocations = "";
            for (int i = 0; i < product.Locations.Count - 1; i++)
            {
                workLocations += (product.Locations[i]) + ", ";
            }
            workLocations += product.Locations[product.Locations.Count - 1];
            return workLocations;
        }
        public void AddWorkLocations(Product product, string[] locations)
        {
            if (locations.Length == 0)
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
        public void ResetWorkLocations()
        {
            if (Locations == null) return;
            Array.Clear(Locations, 0, Locations.Length);
        }
        public Product TryGetProductById(Guid productId)
        {
            foreach (var product in catalog)
            {
                if (product.Id == productId)
                {
                    return product;
                }
            }
            return null;
        }
        public void AddProduct(Product product)
        {
            catalog.Add(product);
        }
        public void UpdateProduct(Product product)
        {
            foreach (var item in catalog)
            {
                if (item.Id == product.Id)
                {
                    item.Name = product.Name;
                    item.Cost = product.Cost;
                    item.Locations = product.Locations;
                    item.Documents = product.Documents;
                    item.Equipment = product.Equipment;
                    item.Description = product.Description;
                    break;
                }
            }
        }
        public void RemoveProduct(Guid productId)
        {
            foreach (var product in catalog)
            {
                if (product.Id == productId)
                {
                    catalog.Remove(product);
                    break;
                }
            }
        }
    }
}