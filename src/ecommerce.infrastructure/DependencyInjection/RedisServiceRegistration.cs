using ecommerce.application.Interfaces;
using ecommerce.infrastructure.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.infrastructure.DependencyInjection
{
    public static class RedisServiceRegistration
    {
        public static IServiceCollection AddRedisServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Add Redis cache service
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "ecommerce_";
            });

            services.AddHealthChecks()
                .AddCheck<RedisHealthCheck>
                ("redis_health", tags: new[] { "database", "cache" });


            services.AddScoped<ICacheService, RedisCacheService>();
            return services;
        }
    }
}
