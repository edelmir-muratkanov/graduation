using Api.Shared;

namespace Api.Domain.Methods.Events;

public class MethodCreatedDomainEvent(Guid methodId) : DomainEvent
{
    public Guid MethodId { get; init; } = methodId;
}