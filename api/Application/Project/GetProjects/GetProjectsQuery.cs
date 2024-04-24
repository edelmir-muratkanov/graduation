﻿using Shared;

namespace Application.Project.GetProjects;

public record GetProjectsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Operator { get; set; }
    public string Type { get; set; }
    public string CollectorType { get; set; }
}

public record GetProjectsQuery : IQuery<PaginatedList<GetProjectsResponse>>
{
    public string SearchTerm { get; set; } = string.Empty;
    public string SortColumn { get; set; } = "CreatedAt";
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
