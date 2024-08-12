using Bootcamp24.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bootcamp24.Infrastructure;
public static class DependencyInjectionService
{
    public static IServiceCollection AddInfraService(this IServiceCollection services, IConfiguration configuration)
    {
        //var connectionString = configuration.GetConnectionString("BootcampDatabase");
        services.AddDbContext<ApplicationDataContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("BootcampDatabase")));

        return services;
    }
}
