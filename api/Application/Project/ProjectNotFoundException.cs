namespace Application.Project;

public sealed class ProjectNotFoundException(Guid projectId)
    : Exception($"The project with id = {projectId} not found")
{
    public Guid ProjectId { get; } = projectId;
}
