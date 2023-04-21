﻿using Data.Entities;
using Data.EntityFramework;
using Data.Interfaces;

namespace Data.Repositories
{
    public class OrderedFoodRepository : IOrderedFoodRepository
    {
        private readonly DataDbContext _dbContext;

        public OrderedFoodRepository(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderedFood> GetOrderedFood => _dbContext.OrderedFoods.ToList();

        public OrderedFood Get(Guid orderedFoodId)
        {
            return _dbContext.OrderedFoods.FirstOrDefault(o => o.Id == orderedFoodId);
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