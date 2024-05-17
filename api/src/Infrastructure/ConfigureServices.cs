using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Properties;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure;

/// <summary>
/// Класс для конфигурации сервисов инфраструктуры.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Метод для добавления инфраструктурных сервисов в контейнер зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        // Добавление MediatR для обработки  запросов
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly));

        // Получение строки подключения к базе данных из конфигурации
        string? connectionString = configuration.GetConnectionString("Database");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "Connection string 'Database' is null or empty.");
        }

        // Добавление фабрики подключений к базе данных в качестве Singleton
        services.AddSingleton(_ =>
            new DbConnectionFactory(
                new NpgsqlDataSourceBuilder(connectionString).Build()));

        // Настройка контекста для чтения данных из базы данных
        services.AddDbContext<ApplicationReadDbContext>(options =>
            options.UseNpgsql(connectionString, o =>
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .UseSnakeCaseNamingConvention()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        // Добавление перехватчика для отслеживания изменений сущностей в контексте записи
        services.AddSingleton<TrackAuditableEntityInterceptor>();

        // Настройка контекста для записи данных в базу данных
        services.AddDbContext<ApplicationWriteDbContext>((sp, options) =>
            options.UseNpgsql(connectionString, o =>
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<TrackAuditableEntityInterceptor>()));

        // Добавление IUnitOfWork как Scoped сервис
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());


        // Добавление репозиториев как Scoped сервисов
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IMethodRepository, MethodRepository>();
        services.AddScoped<IMethodParameterRepository, MethodParameterRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
        services.AddScoped<IProjectMethodRepository, ProjectMethodRepository>();
        services.AddScoped<IProjectParameterRepository, ProjectParameterRepository>();
        services.AddScoped<ICalculationRepository, CalculationRepository>();
        services.AddScoped<ICalculationItemRepository, CalculationItemRepository>();

        // Добавление сервисов для работы с аутентификацией и авторизацией
        services.AddScoped<IPasswordManager, PasswordManager>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

        return services;
    }
}
