using Infrastructure.Database.Models;

namespace Infrastructure.Database.Configurations.Read;

internal sealed class MethodConfiguration : IEntityTypeConfiguration<MethodReadModel>
{
    public void Configure(EntityTypeBuilder<MethodReadModel> builder)
    {
        builder.HasKey(m => m.Id);
        builder.HasIndex(m => m.Name).IsUnique();

        builder.HasMany(m => m.Parameters)
            .WithOne()
            .HasForeignKey(m => m.MethodId);
    }
}
