using Data.EntityFramework;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApp.Infrastructure;

namespace WebApp
{
    public class Program
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=FoodStore;Trusted_Connection=True;";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<IDataDbContext, DataDbContext>(options =>
                options.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("WebApp")), ServiceLifetime.Scoped);

            builder.Services.AddScoped<IFoodRepository, FoodRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderedFoodRepository, OrderedFoodRepository>();

            builder.Services.AddControllersWithViews(options =>
            {
                options.ModelBinderProviders.Insert(0, new CartModelBinderProvider());
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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