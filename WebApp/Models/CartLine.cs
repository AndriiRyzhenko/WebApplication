using Data.Entities;

namespace WebApp.Models;

public class CartLine
{
    public Food Food { get; set; }
    public int Quantity { get; set; }
}