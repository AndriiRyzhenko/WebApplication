using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class Order
{
    public Guid Id { get; set; }

    [Display(Name = "Ім'я")]
    public string FirstName { get; set; }

    [Display(Name = "Прізвище")]
    public string SecondName { get; set; }

    [Display(Name = "Адреса доставки")]
    public string Address { get; set; }

    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Display(Name = "Телефон")]
    public string Phone { get; set; }

    [Display(Name = "Усього (грн)")]
    public decimal TotalPrice { get; set; }

    public ICollection<OrderedFood> OrderedFood { get; set; }
}
