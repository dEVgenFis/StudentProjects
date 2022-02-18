using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Введите имя")]
        [RegularExpression(@"^[A-z,0-9]+\@[A-z]+\.[A-z]+$", ErrorMessage = "Имя должно соответствовать шаблону: -----@-----.---")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}