using Infrastructure.Database.Models;

namespace Infrastructure.Database.Configurations.Read;

internal sealed class UserConfiguration : IEntityTypeConfiguration<UserReadModel>
{
    public void Configure(EntityTypeBuilder<UserReadModel> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Role)
            .HasConversion<string>();
    }
}
