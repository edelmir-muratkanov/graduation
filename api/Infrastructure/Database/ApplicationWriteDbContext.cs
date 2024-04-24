using Application.Abstractions.Data;
using Domain.Methods;
using Domain.Projects;
using Domain.Properties;
using Domain.Users;

namespace Infrastructure.Database;

internal sealed class ApplicationWriteDbContext(DbContextOptions<ApplicationWriteDbContext> options)
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationWriteDbContext).Assembly,
            WriteConfigurationsFilter);
    }

    public static bool WriteConfigurationsFilter(Type type)
    {
        return type.FullName?.Contains("Configurations.Write") ?? false;
    }
}
