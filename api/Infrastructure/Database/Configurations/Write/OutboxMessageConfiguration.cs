namespace Infrastructure.Database.Configurations.Write;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");
        builder.Property(o => o.Content).HasColumnType("jsonb");
    }
}
