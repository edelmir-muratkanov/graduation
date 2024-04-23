using Domain.Methods;
using Infrastructure.Database.Models;

namespace Infrastructure.Database.Configurations.Read;

internal class ProjectMethodConfiguration : IEntityTypeConfiguration<ProjectMethodReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectMethodReadModel> builder)
    {
        builder.HasKey(m => new { m.ProjectId, m.MethodId });


        builder.HasOne<Method>()
            .WithMany()
            .HasForeignKey(m => m.MethodId)
            .IsRequired();
    }
}