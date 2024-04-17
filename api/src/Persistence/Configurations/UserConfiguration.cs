using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .HasConversion(
                x => x.Value,
                v => Email.Create(v).Value)
            .HasMaxLength(Email.MaxLength);

        builder.Property(u => u.Password)
            .HasConversion(
                p => p.Value,
                v => Password.Create(v).Value)
            .HasMaxLength(Password.MaxLength);

        builder.Property(x => x.Role).HasConversion<string>();

        builder.HasIndex(x => x.Email).IsUnique();
    }
}