using Domain.Methods;
using Shared;

namespace Application.Method.GetMethods;

public record GetMethodsParameterResponse
{
    public required string PropertyName { get; set; }
    public ParameterValueGroup? First { get; set; }
    public ParameterValueGroup? Second { get; set; }
}

public record GetMethodsResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required IEnumerable<string> CollectorTypes { get; set; }
    public required List<GetMethodsParameterResponse> Parameters { get; set; }
}

public record GetMethodsQuery : IQuery<PaginatedList<GetMethodsResponse>>
{
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; } = "CreatedAt";
    public SortOrder? SortOrder { get; set; } = Shared.SortOrder.Asc;
    public int? PageNumber { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}