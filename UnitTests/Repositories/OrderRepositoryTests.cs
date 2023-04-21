using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Moq;
using UnitTests.Helpers;

namespace UnitTests.Repositories;

public class OrderRepositoryTests
{
    private readonly Mock<IDataDbContext> _dbContextMock;
    private readonly OrderRepository _repository;

    public OrderRepositoryTests()
    {
        _dbContextMock = new Mock<IDataDbContext>();
        _repository = new OrderRepository(_dbContextMock.Object);
    }

    [Fact]
    public void GetOrders_ReturnsAllOrders()
    {
        // Arrange
        var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid() },
                new Order { Id = Guid.NewGuid() },
                new Order { Id = Guid.NewGuid() }
            };

        var ordersDbSetMock = orders.AsQueryable().BuildMockDbSet();

        _dbContextMock.Setup(m => m.Orders).Returns(ordersDbSetMock);

        // Act
        var result = _repository.GetOrders.ToList();

        // Assert
        Assert.Equal(orders.Count, result.Count);
        Assert.Equal(orders[0].Id, result[0].Id);
        Assert.Equal(orders[1].Id, result[1].Id);
        Assert.Equal(orders[2].Id, result[2].Id);
    }

    [Fact]
    public void Get_ReturnsOrderById()
    {
        // Arrange
        var order = new Order { Id = Guid.NewGuid() };

        var ordersDbSetMock = new List<Order> { order }.AsQueryable().BuildMockDbSet();

        _dbContextMock.Setup(m => m.Orders).Returns(ordersDbSetMock);

        // Act
        var result = _repository.Get(order.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(order.Id, result.Id);
    }

    [Fact]
    public void Add_AddsOrderToDatabase()
    {
        // Arrange
        var order = new Order { Id = Guid.NewGuid() };

        var ordersDbSetMock = new List<Order>().AsQueryable().BuildMockDbSet();
        _dbContextMock.Setup(m => m.Orders).Returns(ordersDbSetMock);

        // Act
        _repository.Add(order);

        // Assert
        _dbContextMock.Verify(m => m.Orders.Add(order), Times.Once);
        _dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
    }
}