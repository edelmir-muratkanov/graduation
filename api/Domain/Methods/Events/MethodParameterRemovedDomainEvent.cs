namespace Domain.Methods.Events;

public sealed record MethodParameterRemovedDomainEvent(Method Method, MethodParameter MethodParameter) : IDomainEvent
{
    public Method Method { get; set; } = Method;
    public MethodParameter MethodParameter { get; set; } = MethodParameter;
}
