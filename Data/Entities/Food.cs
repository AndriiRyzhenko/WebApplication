using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Food
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Будь ласка, введіть назву")]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть виробника")]
        [Display(Name = "Виробник")]
        public string Brand { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Будь ласка, введіть опис")]

        [Display(Name = "Будь ласка, введіть опис")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть категорію товару")]
        [Display(Name = "Категорія")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть ціну")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Будь ласка, введіть позитивне число")]
        [Display(Name = "Ціна (грн)")]
        public decimal Price { get; set; }
    }
}