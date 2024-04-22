using Api.Shared;

namespace Api.Domain.Methods.Events;

public class MethodParameterAddedDomainEvent(Guid methodParameterId) : DomainEvent
{
    public Guid MethodParameterId { get; init; } = methodParameterId;
}