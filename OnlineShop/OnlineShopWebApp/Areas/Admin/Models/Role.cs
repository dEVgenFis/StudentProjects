using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Models
{
    public class Role
    {
        [Required(ErrorMessage = "Заполните поле")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Длина вводимой строки: от 3-х до 10-ти символов")]
        [RegularExpression(@"^[A-z]+$", ErrorMessage = "Поле должно содержать только буквы латинского алфавита")]
        public string Name { get; set; }
    }
}