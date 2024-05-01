namespace Infrastructure.Outbox;

internal sealed record OutboxMessage(
    Guid Id,
    string Name,
    string Content,
    DateTime CreatedAtUtc,
    DateTime? ProccessedAtUtc = null,
    string? Error = null)
{
    public Guid Id { get; set; } = Id;
    public string Name { get; set; } = Name;
    public string Content { get; set; } = Content;
    public DateTime CreatedAtUtc { get; set; } = CreatedAtUtc;
    public DateTime? ProccessedAtUtc { get; set; } = ProccessedAtUtc;
    public string? Error { get; set; } = Error;
}
