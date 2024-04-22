using Api.Domain.Methods.Events;
using Api.Shared;
using Api.Shared.Models;

namespace Api.Domain.Methods;

public class Method : AuditableEntity
{
    private readonly List<MethodParameter> _parameters = [];
    private readonly List<CollectorType> _collectorTypes = [];

    public string Name { get; private set; }
    public IList<CollectorType> CollectorTypes => _collectorTypes.ToList();
    public IEnumerable<MethodParameter> Parameters => _parameters.ToList();


    private Method(Guid id, string name, List<CollectorType> collectorTypes) : base(id)
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
        var method = new Method(Guid.NewGuid(), name, collectorTypes.ToHashSet().ToList());

        method.Raise(new MethodCreatedDomainEvent(method.Id));

        return method;
    }

    public Result ChangeNameAndCollectorTypes(string? name, List<CollectorType>? collectorTypes)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;

        if (collectorTypes is not null)
        {
            _collectorTypes.Clear();

            collectorTypes.ForEach(c => _collectorTypes.Add(c));
        }

        return Result.Success();
    }

    public Result AddParameter(Guid propertyId, ParameterValueGroup? first, ParameterValueGroup? second)
    {
        if (_parameters.Any(p => p.MethodId == Id && p.PropertyId == propertyId))
            return Result.Failure(MethodErrors.DuplicateParameters);

        var result = MethodParameter.Create(Id, propertyId, first, second);

        if (result.IsFailure) return result;

        _parameters.Add(result.Value);

        Raise(new MethodParameterAddedDomainEvent(result.Value.Id));

        return Result.Success();
    }

    public Result<MethodParameter> RemoveParameter(Guid propertyId)
    {
        var parameter = _parameters.FirstOrDefault(p => p.PropertyId == propertyId);
        if (parameter is null) return Result.Failure<MethodParameter>(MethodErrors.NotFoundParameter);

        _parameters.Remove(parameter);

        Raise(new MethodParameterRemovedDomainEvent(parameter.Id));

        return parameter;
    }
}