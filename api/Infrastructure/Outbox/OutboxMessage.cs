namespace Infrastructure.Outbox;

internal sealed record OutboxMessage(
    Guid Id,
    string Name,
    string Content,
    DateTime CreatedAtUtc,
    DateTime? ProccessedAtUtc = null,
    string? Error = null);