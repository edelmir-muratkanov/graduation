namespace Domain.Methods.Events;

public sealed record MethodParameterAddedDomainEvent(Guid MethodId, Guid MethodParameterId) : IDomainEvent
{
    public Guid MethodId { get; set; } = MethodId;
    public Guid MethodParameterId { get; set; } = MethodParameterId;
}
