using Domain.Methods.Events;

namespace Domain.Methods;

/// <summary>
/// Класс, представляющий метод увелечения нефтеотдачи.
/// </summary>
public class Method : AuditableEntity
{
    private readonly List<CollectorType> _collectorTypes = [];
    private readonly List<MethodParameter> _parameters = [];

    /// <summary>
    /// Создает новый метод с указанным именем и типами коллекторов.
    /// </summary>
    /// <param name="id">Идентификатор метода.</param>
    /// <param name="name">Имя метода.</param>
    /// <param name="collectorTypes">Типы коллекторов, связанные с методом.</param>
    private Method(Guid id, string name, List<CollectorType> collectorTypes) : base(id)
    {
        Id = id;
        Name = name;
        _collectorTypes = collectorTypes;
    }

    /// <summary>
    /// Закрытый конструктор без параметров, предотвращающий создание экземпляров класса за его пределами.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    private Method()
    {
    }

    /// <summary>
    /// Название метода.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Список типов коллекторов, связанных с методом.
    /// </summary>
    public IList<CollectorType> CollectorTypes => _collectorTypes.ToList();

    /// <summary>
    /// Список параметров метода.
    /// </summary>
    public IEnumerable<MethodParameter> Parameters => _parameters.ToList();

    /// <summary>
    /// Создает новый метод с указанным именем и типами коллекторов.
    /// </summary>
    /// <param name="name">Имя метода.</param>
    /// <param name="collectorTypes">Типы коллекторов, связанные с методом.</param>
    /// <returns>Результат операции создания метода.</returns>
    public static Result<Method> Create(string name, IEnumerable<CollectorType> collectorTypes)
    {
        var method = new Method(Guid.NewGuid(), name, collectorTypes.ToHashSet().ToList());

        method.Raise(new MethodCreatedDomainEvent(method.Id));

        return method;
    }

    /// <summary>
    /// Изменяет имя и типы коллекторов метода.
    /// </summary>
    /// <param name="name">Новое имя метода (необязательно).</param>
    /// <param name="collectorTypes">Новые типы коллекторов метода (необязательно).</param>
    /// <returns>Результат операции изменения имени и типов коллекторов метода.</returns>
    public Result ChangeNameAndCollectorTypes(string? name, List<CollectorType>? collectorTypes)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }

        if (collectorTypes is not null)
        {
            _collectorTypes.Clear();

            collectorTypes.ForEach(c => _collectorTypes.Add(c));
        }

        Raise(new MethodUpdatedDomainEvent(Id));
        return Result.Success();
    }

    /// <summary>
    /// Добавляет новый параметр к методу.
    /// </summary>
    /// <param name="propertyId">Идентификатор свойства параметра.</param>
    /// <param name="first">Значения первой группы параметра (необязательно).</param>
    /// <param name="second">Значения второй группы параметра (необязательно).</param>
    /// <returns>Результат операции добавления параметра к методу.</returns>
    public Result<MethodParameter> AddParameter(
        Guid propertyId,
        ParameterValueGroup? first,
        ParameterValueGroup? second)
    {
        if (_parameters.Any(p => p.MethodId == Id && p.PropertyId == propertyId))
        {
            return Result.Failure<MethodParameter>(MethodErrors.DuplicateParameters);
        }

        Result<MethodParameter> result = MethodParameter.Create(Id, propertyId, first, second);

        if (result.IsFailure)
        {
            return result;
        }

        _parameters.Add(result.Value);

        Raise(new MethodParameterAddedDomainEvent(Id, result.Value.Id));

        return result;
    }

    /// <summary>
    /// Удаляет параметр метода по его идентификатору.
    /// </summary>
    /// <param name="parameterId">Идентификатор параметра для удаления.</param>
    /// <returns>Результат операции удаления параметра метода.</returns>
    public Result<MethodParameter> RemoveParameter(Guid parameterId)
    {
        MethodParameter? parameter = _parameters.FirstOrDefault(p => p.Id == parameterId);
        if (parameter is null)
        {
            return Result.Failure<MethodParameter>(MethodErrors.NotFoundParameter);
        }

        _parameters.Remove(parameter);

        Raise(new MethodParameterRemovedDomainEvent(Id, parameter.PropertyId));

        return parameter;
    }
}
