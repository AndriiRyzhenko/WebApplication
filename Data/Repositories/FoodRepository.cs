using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class FoodRepository : IFoodRepository
    {

        private List<Food> FoodList = new List<Food>
        {
            new()
            {
                Category = "Category1",
                Description = "Description",
                Id = Guid.NewGuid(),
                Name = "Name1",
                Price = 123
            },
            new()
            {
                Category = "Category2",
                Description = "Description",
                Id = Guid.NewGuid(),
                Name = "Name2",
                Price = 123
            },
            new()
            {
                Category = "Category1",
                Description = "Description",
                Id = Guid.NewGuid(),
                Name = "Name3",
                Price = 123
            },
            new()
            {
                Category = "Category2",
                Description = "Description",
                Id = Guid.NewGuid(),
                Name = "Name4",
                Price = 123
            },
            new()
            {
                Category = "Category3",
                Description = "Description",
                Id = Guid.NewGuid(),
                Name = "Name5",
                Price = 123
            }
        };

        public IEnumerable<Food> GetFood
        {
            get
            {
                return FoodList;
            }
        }

        public void Save(Food food)
        {
            FoodList.Add(food);
        }

        public Food Update(Food food)
        {
            FoodList.Add(food);
            return food;
        }
    }
}
