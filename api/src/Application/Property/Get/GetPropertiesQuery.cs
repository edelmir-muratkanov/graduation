using Shared;

namespace Application.Property.Get;

/// <summary>
/// Ответ на запрос <see cref="GetPropertiesQuery"/>
/// </summary>
public record GetPropertiesResponse(Guid Id, string Name, string Unit);

/// <summary>
/// Запрос на получение списка свойств с пагинацией и сортировкой.
/// </summary>
public record GetPropertiesQuery : IQuery<PaginatedList<GetPropertiesResponse>>
{
    public string? SearchTerm { get; init; } 
    public string? SortColumn { get; init; } 
    public SortOrder? SortOrder { get; init; } 
    public int? PageNumber { get; init; } 
    public int? PageSize { get; init; } 
}
