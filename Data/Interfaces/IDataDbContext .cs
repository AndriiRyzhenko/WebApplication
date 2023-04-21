using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Interfaces;

public interface IDataDbContext
{
    DbSet<Food> Foods { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderedFood> OrderedFoods { get; set; }
    int SaveChanges();
}

