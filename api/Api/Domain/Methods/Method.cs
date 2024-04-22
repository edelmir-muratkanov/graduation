using Api.Shared;
using Api.Shared.Models;

namespace Api.Domain.Methods;

public class Method : AuditableEntity, IHasDomainEvent
{
    private readonly List<MethodParameter> _parameters = [];
    private readonly List<CollectorType> _collectorTypes = [];

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IList<CollectorType> CollectorTypes => _collectorTypes.ToList();
    public IEnumerable<MethodParameter> Parameters => _parameters.ToList();
    public List<DomainEvent> DomainEvents { get; } = [];


    private Method(Guid id, string name, List<CollectorType> collectorTypes)
    {
        Id = id;
        Name = name;
        _collectorTypes = collectorTypes;
    }

    private Method()
    {
    }

    public static Result<Method> Create(string name, IEnumerable<CollectorType> collectorTypes)
    {
        return new Method(Guid.NewGuid(), name, collectorTypes.ToHashSet().ToList());
    }

    public Result AddParameter(Guid propertyId, ParameterValueGroup? first, ParameterValueGroup? second)
    {
        if (_parameters.Any(p => p.MethodId == Id && p.PropertyId == propertyId))
            return Result.Failure(MethodErrors.DuplicateParameters);

        var result = MethodParameter.Create(Id, propertyId, first, second);

        if (result.IsFailure) return result;

        _parameters.Add(result.Value);

        return Result.Success();
    }
}

public class MethodUpdatedDomainEvent(Method method) : DomainEvent
{
    public Method Method { get; init; } = method;
}