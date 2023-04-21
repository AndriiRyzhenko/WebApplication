using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;

namespace UnitTests.Controllers;

public class AdminControllerTests
{
    [Fact]
    public void FoodList_ReturnsViewResult_WithListOfFood()
    {
        // Arrange
        var mockFoodRepository = new Mock<IFoodRepository>();
        var expectedFoods = new List<Food> { new Food { Id = Guid.NewGuid(), Name = "Food 1", Price = 10 } };
        mockFoodRepository.Setup(repo => repo.GetFood).Returns(expectedFoods);
        var controller = new AdminController(mockFoodRepository.Object, null, null);

        // Act
        var result = controller.FoodList();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Food>>(viewResult.ViewData.Model);
        Assert.Equal(expectedFoods, model);
    }

    [Fact]
    public void Add_WhenModelStateIsInvalid_ReturnsViewResultWithFood()
    {
        // Arrange
        var mockFoodRepository = new Mock<IFoodRepository>();
        var foodToAdd = new Food { Name = "Food 1", Price = -10 };
        var controller = new AdminController(mockFoodRepository.Object, null, null);
        controller.ModelState.AddModelError("Price", "Price must be greater than zero");

        // Act
        var result = controller.Add(foodToAdd) as ViewResult;

        // Assert
        Assert.IsType<Food>(result.ViewData.Model);
        Assert.Equal(foodToAdd, result.ViewData.Model);
    }

    [Fact]
    public void Edit_ReturnsViewResult_WithFoodModel()
    {
        // Arrange
        var food = new Food { Id = Guid.NewGuid(), Name = "Test Food", Description = "Test description", Price = 9.99m };
        var mockFoodRepository = new Mock<IFoodRepository>();
        mockFoodRepository.Setup(repo => repo.GetFood).Returns(new List<Food> { food }.AsQueryable());
        var controller = new AdminController(mockFoodRepository.Object, null, null);

        // Act
        var result = controller.Edit(food.Id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Food>(viewResult.ViewData.Model);
        Assert.Equal(food, model);
    }

    [Fact]
    public void Edit_ReturnsViewResult_WithFoodModel_WhenModelStateIsInvalid()
    {
        // Arrange
        var food = new Food { Id = Guid.NewGuid(), Name = "Test Food", Description = "Test description", Price = 9.99m };
        var mockFoodRepository = new Mock<IFoodRepository>();
        var controller = new AdminController(mockFoodRepository.Object, null, null);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = controller.Edit(food);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Food>(viewResult.ViewData.Model);
        Assert.Equal(food, model);
    }

    [Fact]
    public void Delete_RedirectsToFoodList_WhenFoodIsDeleted()
    {
        // Arrange
        var foodId = Guid.NewGuid();
        var mockFoodRepository = new Mock<IFoodRepository>();
        var controller = new AdminController(mockFoodRepository.Object, null, null);

        // Act
        var result = controller.Delete(new Food { Id = foodId });

        // Assert
        mockFoodRepository.Verify(repo => repo.Delete(foodId), Times.Once);
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("FoodList", redirectToActionResult.ActionName);
    }
}
