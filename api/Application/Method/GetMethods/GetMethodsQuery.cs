using Domain;
using Shared;

namespace Application.Method.GetMethods;

public record GetMethodsResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required IEnumerable<CollectorType> CollectorTypes { get; set; }
}

public record GetMethodsQuery : IQuery<PaginatedList<GetMethodsResponse>>
{
    public string? SearchTerm { get; init; } 
    public string? SortColumn { get; init; } 
    public SortOrder? SortOrder { get; init; } 
    public int? PageNumber { get; init; } 
    public int? PageSize { get; init; }
}
