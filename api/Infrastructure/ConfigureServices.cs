using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Domain.Methods;
using Domain.Properties;
using Domain.Users;
using Infrastructure.Authentication;
using Infrastructure.Database;
using Infrastructure.Outbox;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly));

        var connectionString = configuration.GetConnectionString("Database");

        if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

        services.AddSingleton(_ =>
            new DbConnectionFactory(
                new NpgsqlDataSourceBuilder(connectionString).Build()));

        services.AddDbContext<ApplicationReadDbContext>(options =>
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddSingleton<TrackAuditableEntityInterceptor>();
        services.AddSingleton<PublishDomainEventsInterceptor>();
        services.AddSingleton<InsertOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationWriteDbContext>((sp, options) =>
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>(),
                    sp.GetRequiredService<TrackAuditableEntityInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IMethodRepository, MethodRepository>();
        services.AddScoped<IMethodParameterRepository, MethodParameterRepository>();


        services.AddScoped<IPasswordManager, PasswordManager>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

        return services;
    }
}