namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="ProjectReadModel"/> для чтения из базы данных.
/// </summary>
internal class ProjectConfiguration : IEntityTypeConfiguration<ProjectReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectReadModel> builder)
    {
        // Установка первичного ключа
        builder.HasKey(p => p.Id);

        // Определение отношений "один-ко-многим" для связей сущности ProjectReadModel с Parameters, Members и Methods
        // Один проект может иметь много параметров, участников и методов
        builder.HasMany(p => p.Parameters)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Members)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Methods)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();
    }
}
