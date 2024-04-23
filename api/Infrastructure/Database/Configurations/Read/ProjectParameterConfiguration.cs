using Domain.Properties;
using Infrastructure.Database.Models;

namespace Infrastructure.Database.Configurations.Read;

internal class ProjectParameterConfiguration : IEntityTypeConfiguration<ProjectParameterReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectParameterReadModel> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey(p => p.PropertyId)
            .IsRequired();
    }
}