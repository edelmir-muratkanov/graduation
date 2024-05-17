using Domain.Users;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="User"/> для записи в базу данных.
/// </summary>
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Игнорирование поля DomainEvents
        builder.Ignore(u => u.DomainEvents);

        // Установка первичного ключа
        builder.HasKey(u => u.Id);

        // Установка максимальной длины и обязательности для поля Email
        builder.Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();

        // Установка максимальной длины и обязательности для поля Password
        builder.Property(u => u.Password)
            .HasMaxLength(255)
            .IsRequired();

        // Установка обязательности для поля Role и преобразование его к строке
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();

        // Установка максимальной длины для поля Token
        builder.Property(u => u.Token)
            .HasMaxLength(255);

        // Установка уникального индекса для поля Email
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
