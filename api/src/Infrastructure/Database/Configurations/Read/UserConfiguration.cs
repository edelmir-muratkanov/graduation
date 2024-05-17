namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="UserReadModel"/> для чтения из базы данных.
/// </summary>
internal sealed class UserConfiguration : IEntityTypeConfiguration<UserReadModel>
{
    public void Configure(EntityTypeBuilder<UserReadModel> builder)
    {
        // Установка первичного ключа
        builder.HasKey(u => u.Id);
        
        // Установка уникального индекса для поля Email
        builder.HasIndex(u => u.Email).IsUnique();

        // Преобразование перечисления Role в строку для хранения в базе данных
        builder.Property(u => u.Role)
            .HasConversion<string>();
    }
}
