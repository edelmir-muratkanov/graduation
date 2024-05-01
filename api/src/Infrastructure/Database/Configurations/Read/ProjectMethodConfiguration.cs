namespace Infrastructure.Database.Configurations.Read;

internal class ProjectMethodConfiguration : IEntityTypeConfiguration<ProjectMethodReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectMethodReadModel> builder)
    {
        builder.HasKey(p => new { p.MethodId, p.ProjectId });

        builder.HasOne(p => p.Method)
            .WithMany()
            .HasForeignKey(p => p.MethodId)
            .IsRequired();
    }
}
