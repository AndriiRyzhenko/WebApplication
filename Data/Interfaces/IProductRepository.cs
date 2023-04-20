using Data.Entities;

namespace Data.Interfaces;
public interface IFoodRepository
{
    IEnumerable<Food> GetFood { get; }
    void Save(Food food);
    Food Update(Food food);
}

