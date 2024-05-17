namespace Domain.Methods;

/// <summary>
/// Представляет параметр метода увелечения нефтеотдачи.
/// </summary>
public class MethodParameter : Entity
{
    /// <summary>
    /// Создает новый экземпляр параметра метода.
    /// </summary>
    /// <param name="id">Идентификатор параметра.</param>
    /// <param name="methodId">Идентификатор метода, к которому относится параметр.</param>
    /// <param name="propertyId">Идентификатор свойства, связанного с параметром.</param>
    /// <param name="first">Первая группа значений параметра.</param>
    /// <param name="second">Вторая группа значений параметра.</param>
    private MethodParameter(
        Guid id,
        Guid methodId,
        Guid propertyId,
        ParameterValueGroup? first,
        ParameterValueGroup? second) : base(id)
    {
        Id = id;
        MethodId = methodId;
        PropertyId = propertyId;
        FirstParameters = first;
        SecondParameters = second;
    }

    /// <summary>
    /// Закрытый конструктор без параметров, предотвращающий создание экземпляров класса за его пределами.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    private MethodParameter()
    {
    }

    /// <summary>
    /// Идентификатор метода, к которому относится параметр.
    /// </summary>
    public Guid MethodId { get; private set; }

    /// <summary>
    /// Идентификатор свойства, связанного с параметром.
    /// </summary>
    public Guid PropertyId { get; private set; }

    /// <summary>
    /// Первая группа значений параметра.
    /// </summary>
    public ParameterValueGroup? FirstParameters { get; private set; }

    /// <summary>
    /// Вторая группа значений параметра.
    /// </summary>
    public ParameterValueGroup? SecondParameters { get; private set; }

    /// <summary>
    /// Создает новый экземпляр параметра метода.
    /// </summary>
    /// <param name="methodId">Идентификатор метода, к которому относится параметр.</param>
    /// <param name="propertyId">Идентификатор свойства, связанного с параметром.</param>
    /// <param name="first">Первая группа значений параметра.</param>
    /// <param name="second">Вторая группа значений параметра.</param>
    /// <returns>Результат создания параметра метода.</returns>
    public static Result<MethodParameter> Create(
        Guid methodId,
        Guid propertyId,
        ParameterValueGroup? first,
        ParameterValueGroup? second)
    {
        if (first is null && second is null)
        {
            return Result.Failure<MethodParameter>(MethodParameterErrors.MissingFirstAndSecond);
        }

        return new MethodParameter(Guid.NewGuid(), methodId, propertyId, first, second);
    }
}
