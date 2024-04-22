using Domain.Properties;

namespace Infrastructure.Database.Configurations.Write;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Unit)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(p => p.Name).IsUnique();
    }
}