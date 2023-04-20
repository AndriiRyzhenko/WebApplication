using Data.Model;

namespace WebApp.Models;

public class CartIndexViewModel
{
    public Cart Cart { get; set; }
    public string ReturnUrl { get; set; }
}