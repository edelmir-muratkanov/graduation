using Domain;
using Shared;

namespace Application.Method.GetMethods;

/// <summary>
/// Контракт для ответа на запрос <see cref="GetMethodsQuery"/>
/// </summary>
public record GetMethodsResponse
{
    /// <summary>
    /// Идентификатор метода
    /// </summary>
    public required Guid Id { get; init; }
    /// <summary>
    /// Название метода
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Типы коллекторов
    /// </summary>
    public required IEnumerable<CollectorType> CollectorTypes { get; set; }
}

/// <summary>
/// Запрос на получение списка методов
/// </summary>
public record GetMethodsQuery : IQuery<PaginatedList<GetMethodsResponse>>
{
    /// <summary>
    /// Значение, по которому будет поиск
    /// </summary>
    public string? SearchTerm { get; init; } 
    /// <summary>
    /// Значение, по которому будет сортировка
    /// </summary>
    public string? SortColumn { get; init; } 
    /// <summary>
    /// Порядок сортировки
    /// </summary>
    public SortOrder? SortOrder { get; init; } 
    /// <summary>
    /// Количество элементов на странице
    /// </summary>
    public int? PageNumber { get; init; } 
    /// <summary>
    /// Номер страницы
    /// </summary>
    public int? PageSize { get; init; }
}
