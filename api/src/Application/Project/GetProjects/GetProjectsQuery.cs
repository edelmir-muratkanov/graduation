using Domain;
using Domain.Projects;
using Shared;

namespace Application.Project.GetProjects;

public record GetProjectsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Operator { get; set; }
    public ProjectType Type { get; set; }
    public CollectorType CollectorType { get; set; }
}

public record GetProjectsQuery : IQuery<PaginatedList<GetProjectsResponse>>
{
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; }
    public SortOrder? SortOrder { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
