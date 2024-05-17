using Domain.Calculation;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="CalculationItem"/> для записи в базу данных.
/// </summary>
internal sealed class CalculationItemConfiguration : IEntityTypeConfiguration<CalculationItem>
{
    public void Configure(EntityTypeBuilder<CalculationItem> builder)
    {
        // Установка первичного ключа
        builder.HasKey(i => i.Id);
        
        // Установка уникального индекса для комбинации CalculationId и PropertyName
        builder.HasIndex(i => new { i.CalculationId, i.PropertyName }).IsUnique();

        // Преобразование перечислимого типа Belonging при сохранении в базу данных
        builder.Property(i => i.Belonging)
            .HasConversion(
                // Преобразование объекта Belonging в число
                belonging => belonging.Degree,
                // Преобразование числа в объект Belonging
                degree => new Belonging(degree));
    }
}
