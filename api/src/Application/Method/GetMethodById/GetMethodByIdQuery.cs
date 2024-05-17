using Domain;
using Domain.Methods;

namespace Application.Method.GetMethodById;

/// <summary>
/// Ответ на запрос <see cref="GetMethodByIdQuery"/>
/// </summary>
public record GetMethodByIdParameterResponse
{
    /// <summary>
    /// Идентификатор параметра.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Наименование свойства.
    /// </summary>
    public required string PropertyName { get; set; }

    /// <summary>
    /// Единица измерения свойства.
    /// </summary>
    public required string PropertyUnit { get; set; }

    /// <summary>
    /// Первая группа значений параметра.
    /// </summary>
    public ParameterValueGroup? First { get; set; }

    /// <summary>
    /// Вторая группа значений параметра.
    /// </summary>
    public ParameterValueGroup? Second { get; set; }
}

/// <summary>
/// Ответ на запрос <see cref="GetMethodByIdQuery"/>
/// </summary>
public record GetMethodByIdResponse
{
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Наименование метода.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Типы коллекторов метода.
    /// </summary>
    public required List<CollectorType> CollectorTypes { get; set; }

    /// <summary>
    /// Параметры метода.
    /// </summary>
    public required List<GetMethodByIdParameterResponse> Parameters { get; set; }
}

/// <summary>
/// Запрос метода по его идентификатору.
/// </summary>
public record GetMethodByIdQuery : IQuery<GetMethodByIdResponse>
{
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public required Guid Id { get; init; }
}
