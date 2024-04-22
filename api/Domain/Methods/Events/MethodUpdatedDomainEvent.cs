namespace Domain.Methods.Events;

public class MethodUpdatedDomainEvent(Method method) : IDomainEvent
{
    public Method Method { get; init; } = method;
}