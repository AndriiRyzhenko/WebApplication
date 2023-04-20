using Data.Entities;

namespace WebApp.Models;

public class FoodListViewModel
{
    public PagingInfo PagingInfo { get; set; }
    public IEnumerable<Food> Food { get; set; }
    public string CurrentCategory { get; set; }
}

