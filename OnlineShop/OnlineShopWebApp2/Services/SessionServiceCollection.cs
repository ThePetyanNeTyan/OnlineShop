using OnlineShop.Db.Models;
using OnlineShopWebApp.Services.Abstract;

namespace OnlineShopWebApp.Services
{
    public static class SessionServiceCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IService<Cart>, CartService>();
            services.AddTransient<IService<Favorite>, FavoriteService>();
            services.AddTransient<IService<Comparison>, ComparisonService>();
            return services;
        }
    }
}
