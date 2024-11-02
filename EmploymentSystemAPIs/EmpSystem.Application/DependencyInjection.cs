using EmpSystem.Application.BusinessInterfaces;
using EmpSystem.Application.BusinessLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmpSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IVacancyProcessor, VacancyProcessor>();

            return services;
        }
    }
}
