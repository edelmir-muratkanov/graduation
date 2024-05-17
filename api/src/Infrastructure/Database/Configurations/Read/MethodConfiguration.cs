namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="MethodReadModel"/> для чтения из базы данных.
/// </summary>
internal sealed class MethodConfiguration : IEntityTypeConfiguration<MethodReadModel>
{
    public void Configure(EntityTypeBuilder<MethodReadModel> builder)
    {
        // Установка первичного ключа
        builder.HasKey(m => m.Id);
        
        // Создание уникального индекса
        builder.HasIndex(m => m.Name).IsUnique();

        // Определение отношения "один-ко-многим" сущности MethodParameterReadModel
        // Один метод может иметь несколько параметров
        builder.HasMany(m => m.Parameters)
            .WithOne()
            .HasForeignKey(m => m.MethodId);
    }
}
