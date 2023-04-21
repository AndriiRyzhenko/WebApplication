using Data.Entities;
using Data.EntityFramework;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OrderedFoodRepository : IOrderedFoodRepository
    {
        private readonly DataDbContext _dbContext;

        public OrderedFoodRepository(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderedFood> GetOrderedFood => _dbContext.OrderedFoods.Include(of => of.Food).ToList();

        public OrderedFood Get(Guid orderedFoodId)
        {
            return _dbContext.OrderedFoods.Include(of => of.Food).FirstOrDefault(o => o.Id == orderedFoodId);
        }

        public void Save(OrderedFood orderedFood)
        {
            if (orderedFood.Id == Guid.Empty)
            {
                orderedFood.Id = Guid.NewGuid();
                _dbContext.OrderedFoods.Add(orderedFood);
            }
            else
            {
                _dbContext.OrderedFoods.Update(orderedFood);
            }
            _dbContext.SaveChanges();
        }
    }
}
