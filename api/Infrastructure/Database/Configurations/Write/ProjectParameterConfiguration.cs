using Domain.Projects;
using Domain.Properties;

namespace Infrastructure.Database.Configurations.Write;

internal class ProjectParameterConfiguration : IEntityTypeConfiguration<ProjectParameter>
{
    public void Configure(EntityTypeBuilder<ProjectParameter> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => new { p.ProjectId, p.PropertyId }).IsUnique();

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey(p => p.PropertyId);
    }
}