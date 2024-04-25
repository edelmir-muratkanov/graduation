namespace Domain.Methods.Events;

public sealed record MethodUpdatedDomainEvent(Method Method) : IDomainEvent
{
    public Method Method { get; set; } = Method;
}
