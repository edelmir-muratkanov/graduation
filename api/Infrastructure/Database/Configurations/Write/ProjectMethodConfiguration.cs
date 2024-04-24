using Domain.Methods;
using Domain.Projects;

namespace Infrastructure.Database.Configurations.Write;

internal class ProjectMethodConfiguration : IEntityTypeConfiguration<ProjectMethod>
{
    public void Configure(EntityTypeBuilder<ProjectMethod> builder)
    {
        builder.HasKey(p => new { p.ProjectId, p.MethodId });

        builder.HasOne<Method>()
            .WithMany()
            .HasForeignKey(p => p.MethodId);
    }
}
