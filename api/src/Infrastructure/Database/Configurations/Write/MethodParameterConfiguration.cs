using Domain.Methods;
using Domain.Properties;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="MethodParameter"/> для записи в базу данных.
/// </summary>
internal sealed class MethodParameterConfiguration : IEntityTypeConfiguration<MethodParameter>
{
    public void Configure(EntityTypeBuilder<MethodParameter> builder)
    {
        // Установка первичного ключа
        builder.HasKey(mp => mp.Id);

        // Преобразование вложенных свойств в формат JSON при сохранении в базу данных
        builder.OwnsOne(mp => mp.FirstParameters).ToJson();
        builder.OwnsOne(mp => mp.SecondParameters).ToJson();

        // Установка внешнего ключа для связи с сущностью Property
        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey(mp => mp.PropertyId);

        // Установка уникального индекса для комбинации MethodId и PropertyId
        builder.HasIndex(mp => new { mp.MethodId, mp.PropertyId }).IsUnique();
    }
}
