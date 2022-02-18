using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class DeliveryAddress
    {
        [Required(ErrorMessage = "Введите название города")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина названия города: от 2-х до 25-ти символов")]
        public string СityName { get; set; }
        [Required(ErrorMessage = "Введите название улицы")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина названия улицы: от 2-х до 25-ти символов")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "Укажите номер дома")]
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
    }
}