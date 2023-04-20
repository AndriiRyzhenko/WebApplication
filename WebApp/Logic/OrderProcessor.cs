using Data.Model;
using System.Diagnostics;
using WebApp.Interfaces;

namespace WebApp.Logic;

public class OrderProcessor : IOrderProcessor
{
    public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
    {
        foreach (var line in cart.Lines)
        {
            var subtotal = line.Food.Price * line.Quantity;
            Debug.WriteLine("{0} x {1} (итого: {2:0.00})", line.Quantity, line.Food.Name, subtotal);
        }
    }
}