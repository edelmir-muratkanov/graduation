using Domain.Calculation;

namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="CalculationReadModel"/> для операций чтения из базы данных.
/// </summary>
internal sealed class CalculationConfiguration : IEntityTypeConfiguration<CalculationReadModel>
{
    public void Configure(EntityTypeBuilder<CalculationReadModel> builder)
    {
        // Установка первичного ключа
        builder.HasKey(c => c.Id);
        // Создание уникального индекса для сочетания MethodId и ProjectId
        builder.HasIndex(c => new { c.MethodId, c.ProjectId }).IsUnique();

        // Определение отношения "один-ко-многим" с CalculationItemReadModel
        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(i => i.CalculationId)
            .IsRequired();

        // Определение отношения "многие-ко-многим" с MethodReadModel
        builder.HasOne(c => c.Method)
            .WithMany()
            .HasForeignKey(c => c.MethodId)
            .IsRequired();

        // Определение отношения "многие-ко-многим" с ProjectReadModel
        builder.HasOne(c => c.Project)
            .WithMany()
            .HasForeignKey(c => c.ProjectId)
            .IsRequired();
    }
}
