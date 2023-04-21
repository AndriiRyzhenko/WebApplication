using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp.Models;

namespace WebApp.Infrastructure;

public class CartModelBinder : IModelBinder
{
    private const string cartSessionKey = "Cart";

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        Cart cart = bindingContext.HttpContext.Session.GetObject<Cart>(cartSessionKey);

        if (cart == null)
        {
            cart = new Cart();
            bindingContext.HttpContext.Session.SetObject(cartSessionKey, cart);
        }

        bindingContext.Result = ModelBindingResult.Success(cart);
        return Task.CompletedTask;
    }
}