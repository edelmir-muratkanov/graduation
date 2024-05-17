using System.Reflection;
using Application.Abstractions.Behaviours;
using Domain.Calculation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Класс для конфигурации сервисов приложения.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Добавляет сервисы приложения в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <returns>Коллекция сервисов приложения с добавленными сервисами.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly? assembly = typeof(ConfigureServices).Assembly;

        // Добавление MediatR для обработки команд и запросов
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);


            // Добавление поведений 
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
        });

        // Добавление валидаторов из текущей сборки
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        
        // Регистрация службы для работы с расчетами
        services.AddScoped<ICalculationService, CalculationService>();

        return services;
    }
}
