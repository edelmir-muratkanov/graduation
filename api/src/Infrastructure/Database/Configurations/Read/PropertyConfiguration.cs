namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="PropertyReadModel"/> для чтения из базы данных.
/// </summary>
internal sealed class PropertyConfiguration : IEntityTypeConfiguration<PropertyReadModel>
{
    public void Configure(EntityTypeBuilder<PropertyReadModel> builder)
    {
        // Установка первичного ключа
        builder.HasKey(p => p.Id);
        
        // Установка уникального индекса для поля Name
        builder.HasIndex(p => p.Name).IsUnique();
    }
}
