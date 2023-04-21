using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.Helpers;

namespace UnitTests.Repositories;

public class OrderedFoodRepositoryTests
{
    private readonly Mock<IDataDbContext> _dbContextMock;

    private readonly OrderedFoodRepository _repository;

    public OrderedFoodRepositoryTests()
    {
        _dbContextMock = new Mock<IDataDbContext>();

        _repository = new OrderedFoodRepository(_dbContextMock.Object);
    }

    [Fact]
    public void GetOrderedFood_ReturnsAllOrderedFood()
    {
        // Arrange
        var orderedFoods = new List<OrderedFood>
            {
                new OrderedFood { Id = Guid.NewGuid(), Food = new Food { Id = Guid.NewGuid(), Name = "Food 1" } },
                new OrderedFood { Id = Guid.NewGuid(), Food = new Food { Id = Guid.NewGuid(), Name = "Food 2" } },
                new OrderedFood { Id = Guid.NewGuid(), Food = new Food { Id = Guid.NewGuid(), Name = "Food 3" } }
            }.AsQueryable();

        var orderedFoodDbSetMock = orderedFoods.AsQueryable().BuildMockDbSet();

        _dbContextMock.Setup(m => m.OrderedFoods).Returns(orderedFoodDbSetMock);

        // Act
        var result = _repository.GetOrderedFood;

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void Get_ReturnsOrderedFoodById()
    {
        // Arrange
        var orderedFood = new OrderedFood { Id = Guid.NewGuid(), Food = new Food { Id = Guid.NewGuid(), Name = "Food 1" } };

        var orderedFoodDbSetMock = new List<OrderedFood> { orderedFood }.AsQueryable().BuildMockDbSet();

        _dbContextMock.Setup(m => m.OrderedFoods).Returns(orderedFoodDbSetMock);

        // Act
        var result = _repository.Get(orderedFood.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderedFood.Id, result.Id);
    }

    [Fact]
    public void Add_AddsOrderedFoodToDbContextAndSavesChanges()
    {
        // Arrange
        var orderedFood = new OrderedFood { Id = Guid.NewGuid(), Food = new Food { Id = Guid.NewGuid(), Name = "Food 1" } };

        var orderedFoodDbSetMock = new Mock<DbSet<OrderedFood>>();

        _dbContextMock.Setup(m => m.OrderedFoods).Returns(orderedFoodDbSetMock.Object);

        // Act
        _repository.Add(orderedFood);

        // Assert
        orderedFoodDbSetMock.Verify(m => m.Add(orderedFood), Times.Once);
        _dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
    }
}