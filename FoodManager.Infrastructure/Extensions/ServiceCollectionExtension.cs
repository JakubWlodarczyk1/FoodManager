using FoodManager.Domain.Interfaces;
using FoodManager.Infrastructure.Persistence;
using FoodManager.Infrastructure.Repositories;
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

            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
