namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="MethodParameterReadModel"/> для чтения из базы данных.
/// </summary>
internal sealed class MethodParameterConfiguration : IEntityTypeConfiguration<MethodParameterReadModel>
{
    public void Configure(EntityTypeBuilder<MethodParameterReadModel> builder)
    {
        // Установка первичного ключа
        builder.HasKey(p => p.Id);
        // Создание уникального составного индекса 
        builder.HasIndex(p => new { p.MethodId, p.PropertyId }).IsUnique();

        // Определение вложенных объектов FirstParameters и SecondParameters с использованием формата JSON
        builder.OwnsOne(p => p.FirstParameters).ToJson();
        builder.OwnsOne(p => p.SecondParameters).ToJson();

        // Определение отношения "многие-ко-многим" сущностей MethodParameterReadModel и PropertyReadModel
        builder.HasOne(p => p.Property)
            .WithMany()
            .HasForeignKey(p => p.PropertyId);
    }
}
