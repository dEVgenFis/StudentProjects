using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public enum Theme
    {
        [Display(Name = "Светлая")]
        Light,
        [Display(Name = "Тёмная")]
        Dark
    }
}