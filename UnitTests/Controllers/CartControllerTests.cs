using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace UnitTests.Controllers;

public class CartControllerTests
{
    [Fact]
    public void Checkout_ReturnsViewResult_WithShippingDetails()
    {
        // Arrange
        var controller = new CartController(null, null);

        // Act
        var result = controller.Checkout();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<ShippingDetails>(viewResult.Model);
    }

    [Fact]
    public void Checkout_ReturnsViewResult_WithShippingDetails_WhenModelStateIsNotValid()
    {
        // Arrange
        var controller = new CartController(null, null);
        var shippingDetails = new ShippingDetails();
        controller.ModelState.AddModelError("", "Error");

        // Act
        var result = controller.Checkout(new Cart(), shippingDetails);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<ShippingDetails>(viewResult.Model);
    }
}

