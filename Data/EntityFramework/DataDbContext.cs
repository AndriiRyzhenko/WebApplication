using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderedFood> OrderedFoods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Food>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Order>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<OrderedFood>()
            .HasKey(of => new { of.OrderId, of.FoodId });

        modelBuilder.Entity<OrderedFood>()
            .HasOne(of => of.Order)
            .WithMany(o => o.OrderedFood)
            .HasForeignKey(of => of.OrderId);

        modelBuilder.Entity<OrderedFood>()
            .HasOne(of => of.Food)
            .WithMany()
            .HasForeignKey(of => of.FoodId);
    }
}
