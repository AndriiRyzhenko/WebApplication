using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    IFoodRepository repository;
    public AdminController(IFoodRepository repository)
    {
        this.repository = repository;
    }

    public ViewResult Index()
    {
        return View(repository.GetFood);
    }

    public ViewResult FoodList()
    {
        return View(repository.GetFood);
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
            repository.Save(food);
            TempData["message"] = string.Format($"Товар \"{food.Name}\" збережено");
            return RedirectToAction("Index");
        }
        else
        {
            return View(food);
        }
    }

    public ViewResult Edit(Guid id)
    {
        Food food = repository.GetFood.FirstOrDefault(p => p.Id == id);
        return View(food);
    }

    [HttpPost]
    public ActionResult Edit(Food food)
    {
        if (ModelState.IsValid)
        {
            repository.Update(food);
            TempData["message"] = string.Format($"Зміни інформації про товар \"{food.Name}\" збережено");
            return RedirectToAction("Index");
        }
        else
        {
            return View(food);
        }
    }

    [HttpPost]
    public ActionResult Delete(Food food)
    {
        repository.Delete(food.Id);
        return RedirectToAction("Index");
    }
}

