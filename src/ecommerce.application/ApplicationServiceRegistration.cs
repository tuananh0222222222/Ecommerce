using ecommerce.application.Interfaces;
using ecommerce.application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add application services here
            services.AddScoped<IProductService, ProductService>();

            // Add other services as needed
            return services;
        }
    }
}
