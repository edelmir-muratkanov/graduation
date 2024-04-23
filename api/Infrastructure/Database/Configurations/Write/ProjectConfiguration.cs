using Domain.Methods;
using Domain.Projects;
using Domain.Users;

namespace Infrastructure.Database.Configurations.Write;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
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

        builder.Property(p => p.CollectorType)
            .HasConversion<string>();

        builder.Property(p => p.ProjectType)
            .HasConversion<string>();

        builder.HasMany(p => p.Members)
            .WithOne()
            .HasForeignKey(pm => pm.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Parameters)
            .WithOne()
            .HasForeignKey(pp => pp.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Methods)
            .WithOne()
            .HasForeignKey(pm => pm.ProjectId)
            .IsRequired();
    }
}