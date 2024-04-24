using System.Reflection;
using Application.Abstractions.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly? assembly = typeof(ConfigureServices).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);


            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);


        return services;
    }
}
