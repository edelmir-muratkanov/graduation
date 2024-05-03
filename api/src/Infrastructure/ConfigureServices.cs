using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Properties;
using Domain.Users;
using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Quartz;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly));

        string? connectionString = configuration.GetConnectionString("Database");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }


        services.AddSingleton(_ =>
            new DbConnectionFactory(
                new NpgsqlDataSourceBuilder(connectionString).Build()));

        services.AddDbContext<ApplicationReadDbContext>(options =>
            options.UseNpgsql(connectionString, o =>
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .UseSnakeCaseNamingConvention()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddSingleton<TrackAuditableEntityInterceptor>();
        services.AddSingleton<InsertOutboxMessagesInterceptor>();
        services.AddSingleton<PublishDomainEventsInterceptor>();

        services.AddDbContext<ApplicationWriteDbContext>((sp, options) =>
            options.UseNpgsql(connectionString, o =>
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(
                    sp.GetRequiredService<InsertOutboxMessagesInterceptor>(),
                    sp.GetRequiredService<TrackAuditableEntityInterceptor>(),
                    sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());


        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule
                        .WithIntervalInSeconds(10)
                        .RepeatForever()));
        });

        services.AddQuartzHostedService();


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


        services.AddScoped<IPasswordManager, PasswordManager>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

        return services;
    }
}
