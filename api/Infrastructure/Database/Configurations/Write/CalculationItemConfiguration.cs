using Domain.Calculation;

namespace Infrastructure.Database.Configurations.Write;

internal sealed class CalculationItemConfiguration : IEntityTypeConfiguration<CalculationItem>
{
    public void Configure(EntityTypeBuilder<CalculationItem> builder)
    {
        builder.HasKey(i => i.Id);
        builder.HasIndex(i => new { i.CalculationId, i.PropertyName }).IsUnique();

        builder.Property(i => i.Belonging)
            .HasConversion(
                belonging => belonging.Degree,
                degree => new Belonging(degree));
    }
}
