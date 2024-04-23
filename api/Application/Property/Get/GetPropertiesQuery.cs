using Shared;

namespace Application.Property.Get;

public record GetPropertiesResponse(Guid Id, string Name, string Unit);

public record GetPropertiesQuery : IQuery<PaginatedList<GetPropertiesResponse>>
{
    public string SearchTerm { get; set; } = string.Empty;
    public string SortColumn { get; set; } = "CreatedAt";
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}