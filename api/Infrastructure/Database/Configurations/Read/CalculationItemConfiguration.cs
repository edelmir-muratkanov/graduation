using Domain.Calculation;

namespace Infrastructure.Database.Configurations.Read;

internal sealed class CalculationItemConfiguration : IEntityTypeConfiguration<CalculationItemReadModel>
{
    public void Configure(EntityTypeBuilder<CalculationItemReadModel> builder)
    {
        builder.HasKey(i => i.Id);
        builder.HasIndex(i => i.PropertyName).IsUnique();

        builder.Property(i => i.Belonging)
            .HasConversion(
                belonging => belonging.Degree,
                degree => new Belonging(degree));
    }
}
