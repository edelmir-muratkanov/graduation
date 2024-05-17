using Domain.Calculation;

namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="CalculationItemReadModel"/> для чтения из базы данных.
/// </summary>
internal sealed class CalculationItemConfiguration : IEntityTypeConfiguration<CalculationItemReadModel>
{
    public void Configure(EntityTypeBuilder<CalculationItemReadModel> builder)
    {
        // Установка первичного ключа
        builder.HasKey(i => i.Id);
        
        // Создание уникального индекса
        builder.HasIndex(i => i.PropertyName).IsUnique();

        // Преобразование значения Belonging при сохранении в базу данных

        builder.Property(i => i.Belonging)
            .HasConversion(
                // Преобразование экземпляра Belonging в его степень
                belonging => belonging.Degree,
                // Преобразование степени в экземпляр Belonging
                degree => new Belonging(degree));
    }
}
