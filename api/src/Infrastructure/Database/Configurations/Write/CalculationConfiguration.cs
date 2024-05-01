using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Infrastructure.Database.Configurations.Write;

internal sealed class CalculationConfiguration : IEntityTypeConfiguration<Calculation>
{
    public void Configure(EntityTypeBuilder<Calculation> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => new { c.MethodId, c.ProjectId });

        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(i => i.CalculationId);

        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(c => c.ProjectId)
            .IsRequired();

        builder.HasOne<Method>()
            .WithMany()
            .HasForeignKey(c => c.MethodId)
            .IsRequired();
    }
}
