namespace Domain.Methods.Events;

public sealed class MethodParameterAddedDomainEvent(Guid methodParameterId) : IDomainEvent
{
    public Guid MethodParameterId { get; init; } = methodParameterId;
}
