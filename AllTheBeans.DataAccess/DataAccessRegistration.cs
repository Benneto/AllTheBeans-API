using Microsoft.EntityFrameworkCore;
using AllTheBeans.DataAccess;
using AllTheBeans.Core.Interfaces;
using AllTheBeans.DataAccess.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DataAccessRegistration
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IBeanRepository, BeanRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IBeanImageRepository, BeanImageRepository>();

        return services;
    }
}

