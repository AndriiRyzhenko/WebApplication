using Data.Entities;
using Data.EntityFramework;
using Data.Interfaces;

namespace Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataDbContext _dbContext;

    public OrderRepository(DataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Order> GetOrders => _dbContext.Orders.ToList();

    public Order Get(Guid orderId)
    {
        return _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
    }

    public void Save(Order order)
    {
        if (order.Id == Guid.Empty)
        {
            order.Id = Guid.NewGuid();
            _dbContext.Orders.Add(order);
        }
        else
        {
            _dbContext.Orders.Update(order);
        }
        _dbContext.SaveChanges();
    }
}