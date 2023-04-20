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
            TempData["message"] = string.Format("Изменения информации о товаре \"{0}\" сохранены", food.Name);
            return RedirectToAction("Index");
        }
        else
        {
            return View(food);
        }
    }

    [HttpDelete]
    public ActionResult Delete(Food food)
    {
        return null;
    }
}

