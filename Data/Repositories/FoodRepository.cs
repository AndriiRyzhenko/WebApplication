using Data.Entities;
using Data.EntityFramework;
using Data.Interfaces;

namespace Data.Repositories;
public class FoodRepository : IFoodRepository
{
    private readonly DataDbContext _dbContext;

    public FoodRepository(DataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Food> GetFood => _dbContext.Foods;

    public Food Get(Guid foodId) => _dbContext.Foods.Find(foodId);

    public void Save(Food food)
    {
        if (food.Id == Guid.Empty)
        {
            food.Id = Guid.NewGuid();
            _dbContext.Foods.Add(food);
        }
        else
        {
            var existingFood = _dbContext.Foods.Find(food.Id);
            if (existingFood != null)
            {
                existingFood.Name = food.Name;
                existingFood.Description = food.Description;
                existingFood.Category = food.Category;
                existingFood.Price = food.Price;
            }
            else
            {
                _dbContext.Foods.Add(food);
            }
        }
        _dbContext.SaveChanges();
    }

    public Food Update(Food food)
    {
        var existingFood = _dbContext.Foods.Find(food.Id);
        if (existingFood != null)
        {
            existingFood.Name = food.Name;
            existingFood.Description = food.Description;
            existingFood.Category = food.Category;
            existingFood.Price = food.Price;
            _dbContext.SaveChanges();
        }
        return existingFood;
    }

    public void Delete(Guid foodId)
    {
        var food = _dbContext.Foods.Find(foodId);
        if (food != null)
        {
            _dbContext.Foods.Remove(food);
            _dbContext.SaveChanges();
        }
    }
}