namespace Domain.Methods.Events;

public sealed record MethodCreatedDomainEvent(Method Method) : IDomainEvent
{
    public Method Method { get; set; } = Method;
}
