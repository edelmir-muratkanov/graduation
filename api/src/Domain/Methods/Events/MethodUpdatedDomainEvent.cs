namespace Domain.Methods.Events;

public sealed record MethodUpdatedDomainEvent(Guid MethodId) : IDomainEvent
{
    public Guid MethodId { get; set; } = MethodId;
}
