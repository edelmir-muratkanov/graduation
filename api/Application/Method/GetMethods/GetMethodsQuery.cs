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
    public string SearchTerm { get; set; } = string.Empty;
    public string SortColumn { get; set; } = "CreatedAt";
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
