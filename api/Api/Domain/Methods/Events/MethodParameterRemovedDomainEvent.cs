using Api.Shared;

namespace Api.Domain.Methods.Events;

public class MethodParameterRemovedDomainEvent(Guid methodParameterId) : IDomainEvent
{
    public Guid MethodParameterId { get; init; } = methodParameterId;
}