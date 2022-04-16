using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class InMemoryProductsStorage : IProductsStorage
    {
        private readonly List<Product> productsCatalog = new List<Product>
        {
            new Product("Иван И.", 8000, new List<WorkLocation> {new WorkLocation("Самара"), new WorkLocation("Кинель"), new WorkLocation("Новокуйбышевск")}, "Сертификат об окончании курсов подготовки кадров по специальности \"Сварочные работы\" № ... от ...", "Сварочный аппарат модели ... № ...", "...", "/images/Ivan.jpg"),
            new Product("Глеб Г.", 3000, new List<WorkLocation> {new WorkLocation("Кинель") }, "", "", "", "/images/Gleb.jpg"),
            new Product("Петр П.", 5500, new List<WorkLocation> { new WorkLocation("Тольятти") }, "", "Сварочный аппарат модели ... № ...", "", "/images/Petr.jpg"),
            new Product("Ирина И.", 3000, new List<WorkLocation> { new WorkLocation("Самара"), new WorkLocation("Тольятти") }, "", "", "", "/images/Irina.jpg"),
            new Product("Кирилл К.", 1100, new List<WorkLocation>(), "", "Сварочный аппарат модели ... № ...", "", "/images/Kirill.jpg")
        };
        public string SearchWord { get; set; }
        public decimal MinCost
        {
            get
            {
                return productsCatalog.Count.Equals(0) ? default : productsCatalog.Min(product => product.Cost);
            }
        }
        public decimal SearchMinCost { get; set; }
        public decimal MaxCost
        {
            get
            {
                return productsCatalog.Count.Equals(0) ? default : productsCatalog.Max(product => product.Cost);
            }
        }
        public decimal SearchMaxCost { get; set; }
        public string[] SearchWorkLocations { get; set; }
        public List<Product> GetAllProducts()
        {
            return productsCatalog;
        }
        public Product TryGetProductById(Guid productId)
        {
            return productsCatalog.FirstOrDefault(product => product.Id == productId);
        }
        public List<Product> FilteringProducts(string searchWord, string[] locations, decimal minCost, decimal maxCost)
        {
            SearchWord = searchWord is null ? string.Empty : searchWord.ToLower().Trim();
            SearchMinCost = minCost > maxCost ? MinCost : minCost;
            (SearchMaxCost, SearchWorkLocations) = (maxCost, locations);
            if (!SearchWorkLocations.Length.Equals(0))
            {
                return SearchWorkLocations.Select(location => new WorkLocation(location)).SelectMany(location => productsCatalog.Where(product => product.Cost >= SearchMinCost && product.Cost <= SearchMaxCost).Where(product => product.Name.ToLower().Contains(SearchWord)).Where(product => product.Locations.Contains(location))).Distinct().ToList();
            }
            else
            {
                return productsCatalog.Where(product => product.Cost >= SearchMinCost && product.Cost <= SearchMaxCost).Where(product => product.Name.ToLower().Contains(SearchWord)).ToList();
            }
        }
        public void AddProduct(Product product)
        {
            productsCatalog.Add(product);
        }
        public void UpdateProduct(Product product)
        {
            var item = productsCatalog.First(item => item.Id == product.Id);
            item.Name = product.Name;
            item.Cost = product.Cost;
            item.Locations = product.Locations;
            item.Documents = product.Documents;
            item.Equipment = product.Equipment;
            item.Description = product.Description;
        }
        public void RemoveProduct(Product product)
        {
            productsCatalog.Remove(product);
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