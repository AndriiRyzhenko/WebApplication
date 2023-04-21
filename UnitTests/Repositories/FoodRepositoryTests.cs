using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Moq;
using UnitTests.Helpers;

namespace UnitTests.Repositories;
public class FoodRepositoryTests
{
    private readonly Mock<IDataDbContext> _dbContextMock;
    private readonly FoodRepository _foodRepository;

    public FoodRepositoryTests()
    {
        _dbContextMock = new Mock<IDataDbContext>();
        _foodRepository = new FoodRepository(_dbContextMock.Object);
    }

    [Fact]
    public void GetFood_Returns_All_Foods()
    {
        // Arrange
        var foods = new List<Food>()
        {
            new Food { Id = Guid.NewGuid(), Name = "Food 1", Category = "Category 1", Price = 10.00M },
            new Food { Id = Guid.NewGuid(), Name = "Food 2", Category = "Category 2", Price = 15.50M },
            new Food { Id = Guid.NewGuid(), Name = "Food 3", Category = "Category 3", Price = 20.00M }
        }.AsQueryable();


        var foodDbSetMock = foods.AsQueryable().BuildMockDbSet();

        _dbContextMock.Setup(m => m.Foods).Returns(foodDbSetMock);

        // Act
        var result = _foodRepository.GetFood;

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void Get_Returns_Food_By_Id()
    {
        // Arrange
        var foodId = Guid.NewGuid();
        var food = new Food { Id = foodId, Name = "Food 1", Category = "Category 1", Price = 10.00M };
        _dbContextMock.Setup(db => db.Foods.Find(foodId)).Returns(food);

        // Act
        var result = _foodRepository.Get(foodId);

        // Assert
        Assert.Equal(food, result);
    }

    [Fact]
    public void Add_Adds_New_Food()
    {
        // Arrange
        var food = new Food { Name = "Food 1", Category = "Category 1", Price = 10.00M };
        _dbContextMock.Setup(db => db.Foods.Add(food));

        // Act
        _foodRepository.Add(food);

        // Assert
        _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Add_Assigns_New_Id_If_Id_Is_Empty()
    {
        // Arrange
        var food = new Food { Name = "Food 1", Category = "Category 1", Price = 10.00M };
        _dbContextMock.Setup(db => db.Foods.Add(food));

        // Act
        _foodRepository.Add(food);

        // Assert
        Assert.NotEqual(Guid.Empty, food.Id);
        _dbContextMock.Verify(db => db.Foods.Add(food), Times.Once);
        _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Add_Does_Not_Assign_New_Id_If_Id_Is_Not_Empty()
    {
        // Arrange
        var foodId = Guid.NewGuid();
        var food = new Food { Id = foodId, Name = "Food 1", Category = "Category 1", Price = 10.00M };
        _dbContextMock.Setup(db => db.Foods.Add(food));

        // Act
        _foodRepository.Add(food);

        // Assert
        Assert.Equal(foodId, food.Id);
        _dbContextMock.Verify(db => db.Foods.Add(food), Times.Once);
        _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Update_NonExistingFood_ReturnsNull()
    {
        // Arrange
        var food = new Food
        {
            Id = Guid.NewGuid(),
            Name = "New Food",
            Description = "New Food Description",
            Category = "New Category",
            Price = 5.99m
        };

        _dbContextMock.Setup(m => m.Foods.Find(food.Id)).Returns((Food)null);

        // Act
        var result = _foodRepository.Update(food);

        // Assert
        Assert.Null(result);
        _dbContextMock.Verify(m => m.SaveChanges(), Times.Never);
    }

    // Test deleting a food
    [Fact]
    public void Delete_ExistingFood_RemovesFood()
    {
        // Arrange
        var foodId = Guid.NewGuid();
        var food = new Food
        {
            Id = foodId,
            Name = "Existing Food",
            Description = "Existing Food Description",
            Category = "Existing Category",
            Price = 10.99m
        };

        _dbContextMock.Setup(m => m.Foods.Find(foodId)).Returns(food);

        // Act
        _foodRepository.Delete(foodId);

        // Assert
        _dbContextMock.Verify(m => m.Foods.Remove(food), Times.Once);
        _dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
    }

    // Test deleting a non-existing food
    [Fact]
    public void Delete_NonExistingFood_DoesNothing()
    {
        // Arrange
        var foodId = Guid.NewGuid();

        _dbContextMock.Setup(m => m.Foods.Find(foodId)).Returns((Food)null);

        // Act
        _foodRepository.Delete(foodId);

        // Assert
        _dbContextMock.Verify(m => m.Foods.Remove(It.IsAny<Food>()), Times.Never);
        _dbContextMock.Verify(m => m.SaveChanges(), Times.Never);
    }
}