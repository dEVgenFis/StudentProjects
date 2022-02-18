using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ClientContacts
    {
        [Required(ErrorMessage = "Укажите имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Укажите фамилию")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Укажите номер телефона")]
        public string PhoneNumber { get; set; }
    }
}