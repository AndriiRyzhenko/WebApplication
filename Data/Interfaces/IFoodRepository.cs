﻿using Data.Entities;

namespace Data.Interfaces;
public interface IFoodRepository
{
    IEnumerable<Food> GetFood { get; }
    Food Get(Guid foodId);
    void Add(Food food);
    Food Update(Food food);
    void Delete(Guid foodId);
}

