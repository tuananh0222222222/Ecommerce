using Microsoft.Extensions.Configuration;
using Serilog;

namespace ecommerce.infrastructure.Logging
{
    public static class SerilogLogger
    {
        public static void ConfigureLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
