using ecommerce.domain.Interface;
using ecommerce.persistence.SqlServer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
