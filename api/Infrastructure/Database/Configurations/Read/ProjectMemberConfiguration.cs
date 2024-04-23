using Infrastructure.Database.Models;

namespace Infrastructure.Database.Configurations.Read;

internal class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMemberReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectMemberReadModel> builder)
    {
        builder.HasKey(p => new { p.ProjectId, p.MemberId });

        builder.HasOne(p => p.Member)
            .WithMany()
            .HasForeignKey(p => p.MemberId);
    }
}