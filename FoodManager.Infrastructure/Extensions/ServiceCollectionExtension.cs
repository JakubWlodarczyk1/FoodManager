using FoodManager.Domain.Interfaces;
using FoodManager.Infrastructure.Localization;
using FoodManager.Infrastructure.Persistence;
using FoodManager.Infrastructure.Repositories;
using FoodManager.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Configures and registers infrastructure services for the application, including database context, repositories, identity configuration and seeders.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        /// <param name="configuration">The application configuration providing necessary settings.</param>
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoodManagerDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("FoodManager")));

            services.AddScoped<ProductCategorySeeder>();

            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Stores.MaxLengthForKeys = 450;
                })
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddEntityFrameworkStores<FoodManagerDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        }
    }
}
