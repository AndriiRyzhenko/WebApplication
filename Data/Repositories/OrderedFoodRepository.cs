using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OrderedFoodRepository : IOrderedFoodRepository
    {
        private readonly IDataDbContext _dbContext;

        public OrderedFoodRepository(IDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderedFood> GetOrderedFood => _dbContext.OrderedFoods.Include(of => of.Food).ToList();

        public OrderedFood Get(Guid orderedFoodId)
        {
            return _dbContext.OrderedFoods.Include(of => of.Food).FirstOrDefault(o => o.Id == orderedFoodId);
        }

        public void Add(OrderedFood orderedFood)
        {
            if (orderedFood.Id == Guid.Empty)
            {
                orderedFood.Id = Guid.NewGuid();
                _dbContext.OrderedFoods.Add(orderedFood);
            }
            _dbContext.OrderedFoods.Add(orderedFood);

            _dbContext.SaveChanges();
        }
    }
}
