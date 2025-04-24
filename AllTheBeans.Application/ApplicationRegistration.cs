using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AllTheBeans.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistration).Assembly);
        });

        return services;
    }
}
