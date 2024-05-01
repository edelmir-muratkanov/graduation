using Domain.Calculation;

namespace Infrastructure.Database.Configurations.Read;

internal sealed class CalculationConfiguration : IEntityTypeConfiguration<CalculationReadModel>
{
    public void Configure(EntityTypeBuilder<CalculationReadModel> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => new { c.MethodId, c.ProjectId }).IsUnique();

        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(i => i.CalculationId)
            .IsRequired();

        builder.HasOne(c => c.Method)
            .WithMany()
            .HasForeignKey(c => c.MethodId)
            .IsRequired();

        builder.HasOne(c => c.Project)
            .WithMany()
            .HasForeignKey(c => c.ProjectId)
            .IsRequired();
    }
}
