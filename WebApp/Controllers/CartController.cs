using Data.Entities;
using Data.Interfaces;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
public class CartController : Controller
{
    private const string cartSessionKey = "Cart";

    private readonly IFoodRepository _foodRepository;
    private readonly IOrderRepository _orderRepository;
    public CartController(IFoodRepository repository, IOrderRepository orderRepository)
    {
        _foodRepository = repository;
        _orderRepository = orderRepository;
    }
    public ViewResult Index(Cart cart, string returnUrl)
    {
        return View(new CartIndexViewModel
        {
            Cart = cart,
            ReturnUrl = returnUrl
        });
    }

    public RedirectToActionResult AddToCart(Cart cart, Guid id, string returnUrl)
    {
        Food Food = _foodRepository.GetFood
            .FirstOrDefault(p => p.Id == id);

        if (Food != null)
        {
            cart.AddItem(Food, 1);
        }

        HttpContext.Session.SetObject(cartSessionKey, cart);
        return RedirectToAction("Index", new { returnUrl });
    }

    public RedirectToActionResult RemoveFromCart(Cart cart, Guid id, string returnUrl)
    {
        Food Food = _foodRepository.GetFood
            .FirstOrDefault(p => p.Id == id);

        if (Food != null)
        {
            cart.RemoveLine(Food);
        }

        HttpContext.Session.SetObject(cartSessionKey, cart);
        return RedirectToAction("Index", new { returnUrl });
    }

    public PartialViewResult Summary(Cart cart)
    {
        return PartialView(cart);
    }

    public ViewResult Checkout()
    {
        return View(new ShippingDetails());
    }
    [HttpPost]
    public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
    {
        if (cart.Lines.Count() == 0)
        {
            ModelState.AddModelError("", "Извините, корзина пуста!");
        }
        if (ModelState.IsValid)
        {
            var order = new Order
            {
                Address = shippingDetails.Address,
                City = shippingDetails.City,
                Country = shippingDetails.Country,
                Email = shippingDetails.Email,
                FirstName = shippingDetails.FirstName,
                SecondName = shippingDetails.SecondName,
            };

            var orderedFood = cart.Lines.Select(x => new OrderedFood
            {
                Order = order,
                //Food = x.Food,
                FoodId = x.Food.Id,
                //OrderId = order.Id,
                Quantity = x.Quantity,
                Id = Guid.NewGuid()
            });

            order.OrderedFood = orderedFood.ToArray();

            _orderRepository.Save(order);

            //orderRepository.ProcessOrder(cart, shippingDetails);
            cart.Clear();
            HttpContext.Session.SetObject(cartSessionKey, cart);
            return View("Completed");
        }
        else
        {
            return View(new ShippingDetails());
        }

    }
}