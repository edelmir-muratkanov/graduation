using Domain.Methods;

namespace Infrastructure.Database.Configurations.Write;

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
