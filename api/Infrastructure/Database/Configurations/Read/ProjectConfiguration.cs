using Infrastructure.Database.Models;

namespace Infrastructure.Database.Configurations.Read;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<ProjectReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectReadModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Name);

        builder.Property(p => p.ProjectType)
            .HasConversion<string>();

        builder.Property(p => p.CollectorType)
            .HasConversion<string>();

        builder.HasMany(p => p.Parameters)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Methods)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Members)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();
    }
}