using Data.Interfaces;
using Data.Logic;
using Data.Repositories;
using WebApp.Infrastructure;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<IFoodRepository, FoodRepository>();
            builder.Services.AddScoped<IOrderProcessor, OrderProcessor>();

            builder.Services.AddControllersWithViews(options =>
            {
                options.ModelBinderProviders.Insert(0, new CartModelBinderProvider());
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30); // Время ожидания сеанса
                options.Cookie.HttpOnly = true; // Cookie-файл доступен только для HTTP-запросов
                options.Cookie.IsEssential = true; // Состояние сеанса является важным
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Food}/{action=List}/{id?}");

            app.Run();
        }
    }
}