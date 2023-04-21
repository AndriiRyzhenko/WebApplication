using Data.Entities;
using WebApp.Models;

namespace UnitTests;

public class CartTests
{
    [Fact]
    public void AddItem_AddsNewLine_WhenFoodNotInCart()
    {
        // Arrange
        var cart = new Cart();
        var food = new Food { Id = Guid.NewGuid(), Name = "Food 1", Price = 10 };
        var quantity = 2;

        // Act
        cart.AddItem(food, quantity);

        // Assert
        Assert.Single(cart.Lines);
        Assert.Equal(food, cart.Lines.First().Food);
        Assert.Equal(quantity, cart.Lines.First().Quantity);
    }

    [Fact]
    public void AddItem_IncreasesQuantity_WhenFoodAlreadyInCart()
    {
        // Arrange
        var cart = new Cart();
        var food = new Food { Id = Guid.NewGuid(), Name = "Food 1", Price = 10 };
        var quantity1 = 2;
        var quantity2 = 3;

        // Act
        cart.AddItem(food, quantity1);
        cart.AddItem(food, quantity2);

        // Assert
        Assert.Single(cart.Lines);
        Assert.Equal(food, cart.Lines.First().Food);
        Assert.Equal(quantity1 + quantity2, cart.Lines.First().Quantity);
    }

    [Fact]
    public void RemoveLine_RemovesLine_WhenFoodInCart()
    {
        // Arrange
        var cart = new Cart();
        var food1 = new Food { Id = Guid.NewGuid(), Name = "Food 1", Price = 10 };
        var food2 = new Food { Id = Guid.NewGuid(), Name = "Food 2", Price = 20 };
        var quantity1 = 2;
        var quantity2 = 3;
        cart.AddItem(food1, quantity1);
        cart.AddItem(food2, quantity2);

        // Act
        cart.RemoveLine(food1);

        // Assert
        Assert.Single(cart.Lines);
        Assert.Equal(food2, cart.Lines.First().Food);
        Assert.Equal(quantity2, cart.Lines.First().Quantity);
    }

    [Fact]
    public void ComputeTotalValue_CalculatesCorrectly()
    {
        // Arrange
        var cart = new Cart();
        var food1 = new Food { Id = Guid.NewGuid(), Name = "Food 1", Price = 10 };
        var food2 = new Food { Id = Guid.NewGuid(), Name = "Food 2", Price = 20 };
        var quantity1 = 2;
        var quantity2 = 3;
        cart.AddItem(food1, quantity1);
        cart.AddItem(food2, quantity2);

        // Act
        var totalValue = cart.ComputeTotalValue();

        // Assert
        Assert.Equal(quantity1 * food1.Price + quantity2 * food2.Price, totalValue);
    }

    [Fact]
    public void Clear_RemovesAllLines()
    {
        // Arrange
        var cart = new Cart();
        var food1 = new Food { Id = Guid.NewGuid(), Name = "Food 1", Price = 10 };
        var food2 = new Food { Id = Guid.NewGuid(), Name = "Food 2", Price = 20 };
        var quantity1 = 2;
        var quantity2 = 3;
        cart.AddItem(food1, quantity1);
        cart.AddItem(food2, quantity2);

        // Act
        cart.Clear();

        // Assert
        Assert.Empty(cart.Lines);
    }
}
