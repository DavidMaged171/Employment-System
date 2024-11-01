using EmpSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmpSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmploymentSystemDBContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("EmploymentSystem")));
            return services;
        }
    }
}
