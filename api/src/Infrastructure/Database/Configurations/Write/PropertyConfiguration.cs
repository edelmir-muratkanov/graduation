using Domain.Properties;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="Property"/> для записи в базу данных.
/// </summary>
internal sealed class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        // Установка первичного ключа
        builder.HasKey(p => p.Id);

        // Установка максимальной длины и обязательности для поля Name
        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        // Установка максимальной длины и обязательности для поля Unit
        builder.Property(p => p.Unit)
            .HasMaxLength(255)
            .IsRequired();

        // Установка уникального индекса для поля Name
        builder.HasIndex(p => p.Name).IsUnique();
    }
}
