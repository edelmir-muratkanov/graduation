using Domain.Methods;
using Domain.Projects;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="ProjectMethod"/> для записи в базу данных.
/// </summary>
internal sealed class ProjectMethodConfiguration : IEntityTypeConfiguration<ProjectMethod>
{
    public void Configure(EntityTypeBuilder<ProjectMethod> builder)
    {
        // Установка составного первичного ключа
        builder.HasKey(p => new { p.ProjectId, p.MethodId });

        // Установка связи с сущностью Method
        builder.HasOne<Method>()
            .WithMany()
            .HasForeignKey(p => p.MethodId);
    }
}
