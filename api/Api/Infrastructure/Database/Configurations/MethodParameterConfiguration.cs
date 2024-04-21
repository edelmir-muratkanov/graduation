using System.Text.Json;
using Api.Domain.Methods;
using Api.Domain.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Database.Configurations;

public class MethodParameterConfiguration : IEntityTypeConfiguration<MethodParameter>
{
    public void Configure(EntityTypeBuilder<MethodParameter> builder)
    {
        builder.HasKey(mp => new { mp.MethodId, mp.PropertyId });
        builder.OwnsOne(mp => mp.FirstParameters).ToJson();
        builder.OwnsOne(mp => mp.SecondParameters).ToJson();

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey(mp => mp.PropertyId);

        builder.HasIndex(mp => new { mp.MethodId, mp.PropertyId }).IsUnique();
    }
}