using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Models
{
    public enum OrderStatuses
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name = "В обработке")]
        Processed,
        [Display(Name = "Выполняется")]
        Making,
        [Display(Name = "Приостановлен")]
        Paused,
        [Display(Name = "Готов")]
        Made,
        [Display(Name = "В пути")]
        Delivering,
        [Display(Name = "Доставлен")]
        Delivered,
        [Display(Name = "Ожидает завершения")]
        Wait,
        [Display(Name = "Завершён")]
        Сompleted,
        [Display(Name = "Отменён")]
        Cancelled
    }
}