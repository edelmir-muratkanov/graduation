using Domain.Projects;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="Project"/> для записи в базу данных.
/// </summary>
internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        // Установка первичного ключа
        builder.HasKey(p => p.Id);
        
        // Установка индекса для поля Name
        builder.HasIndex(p => p.Name);

        // Установка ограничений на длину и обязательность для полей Name, Country и Operator
        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Country)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Operator)
            .HasMaxLength(255)
            .IsRequired();

        // Установка связей с другими сущностями
        builder.HasMany(p => p.Members)
            .WithOne()
            .HasForeignKey(pm => pm.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Parameters)
            .WithOne()
            .HasForeignKey(pp => pp.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Methods)
            .WithOne()
            .HasForeignKey(pm => pm.ProjectId)
            .IsRequired();
    }
}
