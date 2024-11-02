using FoodManager.Domain.Interfaces;
using FoodManager.Infrastructure.Persistence;
using FoodManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoodManagerDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("FoodManager")));

            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Stores.MaxLengthForKeys = 450;
                })
                .AddEntityFrameworkStores<FoodManagerDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
