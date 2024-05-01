namespace Domain.Methods.Events;

public sealed record MethodParameterRemovedDomainEvent(Guid MethodId, Guid PropertyId) : IDomainEvent
{
    public Guid MethodId { get; set; } = MethodId;
    public Guid PropertyId { get; set; } = PropertyId;
}
