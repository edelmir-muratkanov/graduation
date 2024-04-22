using Infrastructure.Database.Models;

namespace Infrastructure.Database.Configurations.Read;

internal sealed class PropertyConfiguration : IEntityTypeConfiguration<PropertyReadModel>
{
    public void Configure(EntityTypeBuilder<PropertyReadModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Name).IsUnique();
    }
}