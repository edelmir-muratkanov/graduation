using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="Calculation"/> для записи в базу данных.
/// </summary>
internal sealed class CalculationConfiguration : IEntityTypeConfiguration<Calculation>
{
    public void Configure(EntityTypeBuilder<Calculation> builder)
    {
        // Установка первичного ключа
        builder.HasKey(c => c.Id);
        
        // Установка уникального индекса для комбинации MethodId и ProjectId
        builder.HasIndex(c => new { c.MethodId, c.ProjectId });

        // Определение связи "один ко многим" с сущностью CalculationItem
        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(i => i.CalculationId);

        // Определение внешнего ключа на сущность Project
        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(c => c.ProjectId)
            .IsRequired();

        // Определение внешнего ключа на сущность Method
        builder.HasOne<Method>()
            .WithMany()
            .HasForeignKey(c => c.MethodId)
            .IsRequired();
    }
}
