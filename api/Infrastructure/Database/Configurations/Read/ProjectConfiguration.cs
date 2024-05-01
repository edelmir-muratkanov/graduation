namespace Infrastructure.Database.Configurations.Read;

internal class ProjectConfiguration : IEntityTypeConfiguration<ProjectReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectReadModel> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.Parameters)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Members)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();

        builder.HasMany(p => p.Methods)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();
    }
}
