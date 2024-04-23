using Domain.Projects;
using Domain.Users;

namespace Infrastructure.Database.Configurations.Write;

internal class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.HasKey(m => new { m.ProjectId, m.MemberId });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(m => m.MemberId)
            .IsRequired();
    }
}