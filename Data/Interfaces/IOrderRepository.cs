using Data.Entities;

namespace Data.Interfaces;

public interface IOrderRepository
{
    IEnumerable<Order> GetOrders { get; }
    Order Get(Guid orderId);
    void Add(Order order);
}
