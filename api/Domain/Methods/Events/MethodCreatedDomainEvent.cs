namespace Domain.Methods.Events;

public sealed class MethodCreatedDomainEvent(Guid methodId) : IDomainEvent
{
    public Guid MethodId { get; init; } = methodId;
}
