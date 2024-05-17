namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="ProjectMethodReadModel"/> для чтения из базы данных.
/// </summary>
internal class ProjectMethodConfiguration : IEntityTypeConfiguration<ProjectMethodReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectMethodReadModel> builder)
    {
        // Установка составного первичного ключа
        builder.HasKey(p => new { p.MethodId, p.ProjectId });

        // Определение отношения "многие-ко-многим" между проектами и методами
        // Один метод может быть связан с несколькими проектами, и наоборот
        builder.HasOne(p => p.Method)
            .WithMany()
            .HasForeignKey(p => p.MethodId)
            .IsRequired();
    }
}
