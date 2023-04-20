using Data.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Infrastructure;

public class CartModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return context.Metadata.ModelType == typeof(Cart) ? new CartModelBinder() : null;
    }
}