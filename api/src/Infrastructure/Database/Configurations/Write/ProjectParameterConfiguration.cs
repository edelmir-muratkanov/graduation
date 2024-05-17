using Domain.Projects;
using Domain.Properties;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="ProjectParameter"/> для записи в базу данных.
/// </summary>
internal sealed class ProjectParameterConfiguration : IEntityTypeConfiguration<ProjectParameter>
{
    public void Configure(EntityTypeBuilder<ProjectParameter> builder)
    {
        // Установка первичного ключа
        builder.HasKey(p => p.Id);

        // Установка уникального индекса для сочетания ProjectId и PropertyId
        builder.HasIndex(p => new { p.ProjectId, p.PropertyId }).IsUnique();

        // Установка внешнего ключа на сущность Property
        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey(p => p.PropertyId);
    }
}
