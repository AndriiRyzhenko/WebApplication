using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class FoodController : Controller
    {
        private IFoodRepository _repository;
        public int pageSize = 5;
        public FoodController(IFoodRepository repository)
        {
            _repository = repository;
        }
        public ViewResult List(string category, int page = 1)
        {
            var model = new FoodListViewModel
            {
                Food = _repository.GetFood
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(food => food.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        _repository.GetFood.Count() :
                        _repository.GetFood.Count(p => p.Category == category)
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}
