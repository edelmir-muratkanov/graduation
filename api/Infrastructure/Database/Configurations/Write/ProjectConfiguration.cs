using Domain.Projects;

namespace Infrastructure.Database.Configurations.Write;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Name);

        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Country)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Operator)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.ProjectType)
            .HasConversion<string>();

        builder.Property(p => p.CollectorType)
            .HasConversion<string>();

        builder.HasMany(p => p.Parameters)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Members)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Methods)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();
    }
}