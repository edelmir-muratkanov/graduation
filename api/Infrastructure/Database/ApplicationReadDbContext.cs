using Infrastructure.Database.Models;

namespace Infrastructure.Database;

internal sealed class ApplicationReadDbContext(DbContextOptions<ApplicationReadDbContext> options)
    : DbContext(options)
{
    public DbSet<UserReadModel> Users => Set<UserReadModel>();
    public DbSet<MethodReadModel> Methods => Set<MethodReadModel>();
    public DbSet<MethodParameterReadModel> MethodParameters => Set<MethodParameterReadModel>();
    public DbSet<PropertyReadModel> Properties => Set<PropertyReadModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationReadDbContext).Assembly,
            ReadConfigurationsFilter);
    }

    private static bool ReadConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Read") ?? false;
}