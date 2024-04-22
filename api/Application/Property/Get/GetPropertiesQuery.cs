using Shared;
using SortOrder = Shared.SortOrder;

namespace Application.Property.Get;

public record GetPropertiesResponse(Guid Id, string Name, string Unit);

public record GetPropertiesQuery(
    string? SearchTerm,
    string? SortColumn = "createdAt",
    SortOrder? SortOrder = SortOrder.Asc,
    int PageNumber = 1,
    int PageSize = 10)
    : IQuery<PaginatedList<GetPropertiesResponse>>;