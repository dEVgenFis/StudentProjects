using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Укажите имя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина имени: от 2-х до 25-ти символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите стоимость")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Стоимость: от 1-ой до 2-х цифр после точки")]
        public decimal Cost { get; set; }
        public List<WorkLocation> Locations { get; set; }
        public string Documents { get; set; }
        public string Equipment { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; }
        public Product()
        {
            Id = Guid.NewGuid();
        }
        public Product(string name, decimal cost, List<WorkLocation> locations, string documents, string equipment, string description, string imagePath) : this()
        {
            Name = name;
            Cost = cost;
            Locations = locations;
            Documents = documents;
            Equipment = equipment;
            Description = description;
            ImagePath = imagePath;
        }
    }
}