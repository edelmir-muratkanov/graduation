using Api.Shared;

namespace Api.Domain.Methods;

public class Method : AuditableEntity, IHasDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public List<CollectorType> CollectorTypes { get; set; } = [];
    public List<MethodParameter> Parameters { get; set; } = [];
    public List<DomainEvent> DomainEvents { get; } = [];
}

public class MethodUpdatedDomainEvent(Method method) : DomainEvent
{
    public Method Method { get; init; } = method;
}