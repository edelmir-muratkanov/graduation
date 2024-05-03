using Domain;
using Domain.Projects;

namespace Api.Contracts.Project;

public record UpdateProject
{
    public string? Name { get; init; }
    public string? Country { get; init; }
    public string? Operator { get; init; }
    public ProjectType? Type { get; init; }
    public CollectorType? CollectorType { get; init; }
}
