using Infrastructure.Database.Models;

namespace Infrastructure.Database.Configurations.Read;

internal sealed class MethodParameterConfiguration : IEntityTypeConfiguration<MethodParameterReadModel>
{
    public void Configure(EntityTypeBuilder<MethodParameterReadModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => new { p.MethodId, p.PropertyId }).IsUnique();

        builder.OwnsOne(p => p.First).ToJson();
        builder.OwnsOne(p => p.Second).ToJson();

        builder.HasOne(p => p.Property)
            .WithMany()
            .HasForeignKey(p => p.PropertyId);
    }
}