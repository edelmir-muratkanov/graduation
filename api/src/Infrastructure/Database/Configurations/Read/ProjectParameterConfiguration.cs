namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="ProjectParameterReadModel"/> для чтения из базы данных.
/// </summary>
internal class ProjectParameterConfiguration : IEntityTypeConfiguration<ProjectParameterReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectParameterReadModel> builder)
    {
        // Установка первичного ключа
        builder.HasKey(p => p.Id);

        // Определение внешнего ключа для связи с сущностью PropertyReadModel
        // Один параметр проекта связан с одним свойством
        builder.HasOne(p => p.Property)
            .WithMany()
            .HasForeignKey(p => p.PropertyId)
            .IsRequired();
    }
}
