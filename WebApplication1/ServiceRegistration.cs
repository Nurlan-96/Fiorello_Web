using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Services;
using WebApplication1.Services.Interfaces;

namespace WebApplication1
{
    public static class ServiceRegistration
    {
        public static void Register (this IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
            {
            options.UseSqlServer(config.GetConnectionString("Default"));
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
            });
            services.AddHttpContextAccessor();
            services.AddScoped<IBasketService, BasketService> ();
        }
    }
}
