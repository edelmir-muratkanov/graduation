namespace Domain.Methods.Events;

public class MethodCreatedDomainEvent(Guid methodId) : IDomainEvent
{
    public Guid MethodId { get; init; } = methodId;
}