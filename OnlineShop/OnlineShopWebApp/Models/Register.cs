using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Введите имя")]
        [RegularExpression(@"^[A-z,0-9]+\@[A-z]+\.[A-z]+$", ErrorMessage = "Имя должно соответствовать шаблону: -----@-----.---")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}