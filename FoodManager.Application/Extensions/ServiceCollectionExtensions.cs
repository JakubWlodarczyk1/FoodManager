using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.Mappings;
using FoodManager.Application.Product.Commands.CreateProduct;

namespace FoodManager.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new ProductMappingProfile(userContext));
            }).CreateMapper()
            );

            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssemblyContaining(typeof(CreateProductCommand)));

            services.AddAutoMapper(typeof(ProductMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
