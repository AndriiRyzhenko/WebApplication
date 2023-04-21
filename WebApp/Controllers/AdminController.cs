using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    IFoodRepository _foodRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderedFoodRepository _orderedFoodRepository;

    public AdminController(IFoodRepository foodRepository, IOrderRepository orderRepository, IOrderedFoodRepository orderedFoodRepository)
    {
        _foodRepository = foodRepository;
        _orderRepository = orderRepository;
        _orderedFoodRepository = orderedFoodRepository;
    }

    public ActionResult Index()
    {
        return RedirectToAction("FoodList");
    }

    public ViewResult FoodList()
    {
        return View(_foodRepository.GetFood);
    }

    public ViewResult OrderList()
    {
        return View(_orderRepository.GetOrders);
    }

    public ViewResult OrderedFoodList(Guid id)
    {
        var orderedFood = _orderedFoodRepository.GetOrderedFood.Where(p => p.OrderId == id).ToList();
        return View(orderedFood);
    }

    public ViewResult Add()
    {
        return View(new Food());
    }

    [HttpPost]
    public ActionResult Add(Food food)
    {
        if (ModelState.IsValid)
        {
            _foodRepository.Save(food);
            TempData["message"] = string.Format($"Товар \"{food.Name}\" збережено");
            return RedirectToAction("FoodList");
        }
        else
        {
            return View(food);
        }
    }

    public ViewResult Edit(Guid id)
    {
        Food food = _foodRepository.GetFood.FirstOrDefault(p => p.Id == id);
        return View(food);
    }

    [HttpPost]
    public ActionResult Edit(Food food)
    {
        if (ModelState.IsValid)
        {
            _foodRepository.Update(food);
            TempData["message"] = string.Format($"Зміни інформації про товар \"{food.Name}\" збережено");
            return RedirectToAction("FoodList");
        }
        else
        {
            return View(food);
        }
    }

    [HttpPost]
    public ActionResult Delete(Food food)
    {
        _foodRepository.Delete(food.Id);
        return RedirectToAction("FoodList");
    }
}

