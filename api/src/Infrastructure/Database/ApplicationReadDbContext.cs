namespace Infrastructure.Database;

/// <summary>
/// Контекст базы данных для операций чтения.
/// </summary>
/// <param name="options">Параметры контекста базы данных.</param>
internal sealed class ApplicationReadDbContext(DbContextOptions<ApplicationReadDbContext> options)
    : DbContext(options)
{
    public DbSet<UserReadModel> Users => Set<UserReadModel>();
    public DbSet<MethodReadModel> Methods => Set<MethodReadModel>();
    public DbSet<MethodParameterReadModel> MethodParameters => Set<MethodParameterReadModel>();
    public DbSet<PropertyReadModel> Properties => Set<PropertyReadModel>();
    public DbSet<ProjectReadModel> Projects => Set<ProjectReadModel>();
    public DbSet<ProjectMethodReadModel> ProjectMethods => Set<ProjectMethodReadModel>();
    public DbSet<ProjectMemberReadModel> ProjectMembers => Set<ProjectMemberReadModel>();
    public DbSet<ProjectParameterReadModel> ProjectParameters => Set<ProjectParameterReadModel>();
    public DbSet<CalculationReadModel> Calculations => Set<CalculationReadModel>();
    public DbSet<CalculationItemReadModel> CalculationItems => Set<CalculationItemReadModel>();

    /// <summary>
    /// Настройка отображения сущностей базы данных для операций чтения.
    /// </summary>
    /// <param name="modelBuilder">Строитель модели сущностей.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationReadDbContext).Assembly,
            ReadConfigurationsFilter);
    }

    /// <summary>
    /// Фильтр для настройки отображения сущностей чтения.
    /// </summary>
    /// <param name="type">Тип сущности.</param>
    /// <returns>Значение true, если тип сущности должен быть настроен для чтения, иначе - false.</returns>
    private static bool ReadConfigurationsFilter(Type type)
    {
        return type.FullName?.Contains("Configurations.Read") ?? false;
    }
}
