namespace Domain.Methods.Events;

public sealed class MethodUpdatedDomainEvent(Method method) : IDomainEvent
{
    public Method Method { get; init; } = method;
}
