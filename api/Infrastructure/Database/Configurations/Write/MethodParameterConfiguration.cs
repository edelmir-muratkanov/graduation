using Domain.Methods;
using Domain.Properties;

namespace Infrastructure.Database.Configurations.Write;

public class MethodParameterConfiguration : IEntityTypeConfiguration<MethodParameter>
{
    public void Configure(EntityTypeBuilder<MethodParameter> builder)
    {
        builder.HasKey(mp => mp.Id);
        builder.OwnsOne(mp => mp.FirstParameters).ToJson();
        builder.OwnsOne(mp => mp.SecondParameters).ToJson();

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey(mp => mp.PropertyId);

        builder.HasIndex(mp => new { mp.MethodId, mp.PropertyId }).IsUnique();
    }
}
