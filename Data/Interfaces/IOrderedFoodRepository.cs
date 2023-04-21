using Data.Entities;

namespace Data.Interfaces;

public interface IOrderedFoodRepository
{
    IEnumerable<OrderedFood> GetOrderedFood { get; }
    OrderedFood Get(Guid orderedFoodId);
    void Add(OrderedFood orderedFood);
}
