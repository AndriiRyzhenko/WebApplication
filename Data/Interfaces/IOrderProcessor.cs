using Data.Model;

namespace Data.Interfaces;

public interface IOrderProcessor
{
    void ProcessOrder(Cart cart, ShippingDetails shippingDerails);
}