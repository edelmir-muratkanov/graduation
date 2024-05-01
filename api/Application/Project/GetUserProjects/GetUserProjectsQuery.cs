using Domain;
using Domain.Projects;

namespace Application.Project.GetUserProjects;

public record ProjectResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Operator { get; set; }
    public required ProjectType Type { get; set; }
    public required CollectorType CollectorType { get; set; }
}

public record GetUserProjectsQuery : IQuery<List<ProjectResponse>>;
