using Domain.Projects;
using Domain.Users;

namespace Infrastructure.Database.Configurations.Write;

internal class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.HasKey(p => new { p.ProjectId, p.MemberId });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.MemberId)
            .IsRequired();
    }
}