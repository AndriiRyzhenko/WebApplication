using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class OrderedFood
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid FoodId { get; set; }

    [Display(Name = "Кількість")]
    public int Quantity { get; set; }
    public Order Order { get; set; }
    public Food Food { get; set; }
}

