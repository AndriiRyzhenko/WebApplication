using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class OrderController : Controller
{
    IOrderRepository repository;
    public OrderController(IOrderRepository repository)
    {
        this.repository = repository;
    }
}

