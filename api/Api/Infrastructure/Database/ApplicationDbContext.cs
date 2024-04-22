using System.Reflection;
using Api.Domain.Methods;
using Api.Domain.Properties;
using Api.Domain.Users;
using Api.Shared;
using Api.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Method> Methods => Set<Method>();
    public DbSet<MethodParameter> MethodParameters => Set<MethodParameter>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}