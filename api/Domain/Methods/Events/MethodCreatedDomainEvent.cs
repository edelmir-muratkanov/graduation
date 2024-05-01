namespace Domain.Methods.Events;

public sealed record MethodCreatedDomainEvent(Guid MethodId) : IDomainEvent
{
    public Guid MethodId { get; set; } = MethodId;
}
