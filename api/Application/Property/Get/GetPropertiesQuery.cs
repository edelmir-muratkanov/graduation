using Shared;

namespace Application.Property.Get;

public record GetPropertiesResponse(Guid Id, string Name, string Unit);

public record GetPropertiesQuery : IQuery<PaginatedList<GetPropertiesResponse>>
{
    public string? SearchTerm { get; init; } 
    public string? SortColumn { get; init; } 
    public SortOrder? SortOrder { get; init; } 
    public int? PageNumber { get; init; } 
    public int? PageSize { get; init; } 
}
