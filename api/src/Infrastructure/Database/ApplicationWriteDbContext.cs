using Application.Abstractions.Data;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Properties;
using Domain.Users;

namespace Infrastructure.Database;

/// <summary>
/// Контекст базы данных для операций записи.
/// </summary>
/// <param name="options">Параметры контекста базы данных.</param>
public sealed class ApplicationWriteDbContext(DbContextOptions<ApplicationWriteDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Method> Methods => Set<Method>();
    public DbSet<MethodParameter> MethodParameters => Set<MethodParameter>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectMethod> ProjectMethods => Set<ProjectMethod>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
    public DbSet<ProjectParameter> ProjectParameters => Set<ProjectParameter>();
    public DbSet<Calculation> Calculations => Set<Calculation>();
    public DbSet<CalculationItem> CalculationItems => Set<CalculationItem>();

    /// <summary>
    /// Настройка отображения сущностей базы данных.
    /// </summary>
    /// <param name="builder">Строитель моделей сущностей.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationWriteDbContext).Assembly,
            WriteConfigurationsFilter);
    }

    /// <summary>
    /// Фильтр для настройки отображения сущностей записи.
    /// </summary>
    /// <param name="type">Тип сущности.</param>
    /// <returns>Значение true, если тип сущности должен быть настроен для записи, иначе - false.</returns>
    private static bool WriteConfigurationsFilter(Type type)
    {
        return type.FullName?.Contains("Configurations.Write") ?? false;
    }
}
