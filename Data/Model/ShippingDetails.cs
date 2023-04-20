using System.ComponentModel.DataAnnotations;

namespace Data.Model;

public class ShippingDetails
{
    [Required(ErrorMessage = "Вкажіть Ваше ім'я")]
    [Display(Name = "Ім'я")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Вкажіть Ваше прізвище")]
    [Display(Name = "Прізвище")]
    public string SecondName { get; set; }
    [Required(ErrorMessage = "Вкажіть адресу доставки")]
    [Display(Name = "Адреса доставки")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Вкажіть Ваше місто")]
    [Display(Name = "Місто доставки")]
    public string City { get; set; }
    [Required(ErrorMessage = "Вкажіть Вашу країну")]
    [Display(Name = "Країна доставки")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Email")]
    [Display(Name = "E-mail")]
    public string Email { get; set; }
}