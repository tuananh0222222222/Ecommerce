using ecommerce.application;
using ecommerce.persistence;
using ecommerce.persistence.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplicationServices();
            services.AddPersistenceServices();

            //add connect sql
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOption =>
                {
                    sqlOption.EnableRetryOnFailure(
                        maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null
                        );
                });
            });


            return services;
        }
    }
}
