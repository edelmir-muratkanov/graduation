using Domain.Users;
using Infrastructure.Database.Models;

namespace Infrastructure.Database.Configurations.Read;

internal class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMemberReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectMemberReadModel> builder)
    {
        builder.HasKey(m => new { m.ProjectId, m.MemberId });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(m => m.MemberId)
            .IsRequired();
    }
}