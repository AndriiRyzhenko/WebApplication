using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDataDbContext _dbContext;

    public OrderRepository(IDataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Order> GetOrders => _dbContext.Orders.ToList();

    public Order Get(Guid orderId)
    {
        return _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
    }

    public void Add(Order order)
    {
        if (order.Id == Guid.Empty)
        {
            order.Id = Guid.NewGuid();
        }

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }
}