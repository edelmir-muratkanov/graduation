using Api.Shared;

namespace Api.Domain.Methods.Events;

public class MethodCreatedDomainEvent(Guid methodId) : IDomainEvent
{
    public Guid MethodId { get; init; } = methodId;
}