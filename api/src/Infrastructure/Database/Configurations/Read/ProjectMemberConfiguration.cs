namespace Infrastructure.Database.Configurations.Read;

/// <summary>
/// Конфигурация модели <see cref="ProjectMemberReadModel"/> для чтения из базы данных.
/// </summary>
internal class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMemberReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectMemberReadModel> builder)
    {
        // Установка составного первичного ключа
        builder.HasKey(p => new { p.ProjectId, p.MemberId });

        // Определение отношения "многие-ко-многим" между проектами и участниками
        // Один участник может быть членом нескольких проектов, и наоборот
        builder.HasOne(p => p.Member)
            .WithMany()
            .HasForeignKey(p => p.MemberId);
    }
}
