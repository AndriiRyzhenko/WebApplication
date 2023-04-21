using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

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

    [Required(ErrorMessage = "Email")]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Телефон")]
    [Display(Name = "Телефон")]
    public string Phone { get; set; }
}