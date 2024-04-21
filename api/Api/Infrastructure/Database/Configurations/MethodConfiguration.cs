using Api.Domain.Methods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Database.Configurations;

public class MethodConfiguration : IEntityTypeConfiguration<Method>
{
    public void Configure(EntityTypeBuilder<Method> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Ignore(m => m.DomainEvents);

        builder.Property(m => m.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(m => m.CollectorTypes).IsRequired();

        builder.HasMany(m => m.Parameters)
            .WithOne()
            .HasForeignKey(mp => mp.MethodId)
            .IsRequired();

        builder.HasIndex(m => m.Name).IsUnique();
    }
}