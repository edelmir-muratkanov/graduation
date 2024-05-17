using Domain.Methods;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="Method"/> для записи в базу данных.
/// </summary>
internal sealed class MethodConfiguration : IEntityTypeConfiguration<Method>
{
    public void Configure(EntityTypeBuilder<Method> builder)
    {
        // Установка первичного ключа
        builder.HasKey(m => m.Id);

        // Игнорирование свойства DomainEvents при сохранении в базу данных
        builder.Ignore(m => m.DomainEvents);

        // Установка ограничений на поле Name
        builder.Property(m => m.Name)
            .HasMaxLength(255)
            .IsRequired();

        // Установка ограничений на поле CollectorTypes
        builder.Property(m => m.CollectorTypes).IsRequired();

        // Установка внешнего ключа для связи с сущностью MethodParameter
        builder.HasMany(m => m.Parameters)
            .WithOne()
            .HasForeignKey(mp => mp.MethodId)
            .IsRequired();

        // Установка уникального индекса для поле Name
        builder.HasIndex(m => m.Name).IsUnique();
    }
}
