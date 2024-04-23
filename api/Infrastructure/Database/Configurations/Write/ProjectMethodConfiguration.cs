using Domain.Methods;
using Domain.Projects;

namespace Infrastructure.Database.Configurations.Write;

internal class ProjectMethodConfiguration : IEntityTypeConfiguration<ProjectMethod>
{
    public void Configure(EntityTypeBuilder<ProjectMethod> builder)
    {
        builder.HasKey(m => new { m.ProjectId, m.MethodId });


        builder.HasOne<Method>()
            .WithMany()
            .HasForeignKey(m => m.MethodId)
            .IsRequired();
    }
}