using Domain.Projects;
using Domain.Users;

namespace Infrastructure.Database.Configurations.Write;

/// <summary>
/// Конфигурация сущности <see cref="ProjectMember"/> для записи в базу данных.
/// </summary>
internal sealed class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        // Установка составного первичного ключа
        builder.HasKey(p => new { p.ProjectId, p.MemberId });

        // Установка связи с сущностью User
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.MemberId)
            .IsRequired();
    }
}
