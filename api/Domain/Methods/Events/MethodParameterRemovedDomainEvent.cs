namespace Domain.Methods.Events;

public sealed class MethodParameterRemovedDomainEvent(Guid methodParameterId) : IDomainEvent
{
    public Guid MethodParameterId { get; init; } = methodParameterId;
}
