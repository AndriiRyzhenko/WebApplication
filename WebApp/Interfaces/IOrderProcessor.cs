using Data.Model;

namespace WebApp.Interfaces;

public interface IOrderProcessor
{
    void ProcessOrder(Cart cart, ShippingDetails shippingDerails);
}