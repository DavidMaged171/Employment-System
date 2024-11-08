using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmpSystem.Logging
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(builder=>
            {
                builder.AddFile("Logs/app.log");
            });

            return services;
        }
    }
}
