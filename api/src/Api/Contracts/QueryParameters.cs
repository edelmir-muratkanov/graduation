using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Api.Contracts;

public record QueryParameters
{
    [FromQuery]
    public string? SearchTerm { get; init; }

    [FromQuery]
    public int? PageSize { get; init; }

    [FromQuery]
    public int? PageNumber { get; init; }

    [FromQuery]
    public string? SortColumn { get; init; }

    [FromQuery]
    public SortOrder? SortOrder { get; init; }
}
