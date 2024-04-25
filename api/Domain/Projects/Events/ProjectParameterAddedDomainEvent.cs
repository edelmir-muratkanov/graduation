namespace Domain.Projects.Events;

public sealed record ProjectParameterAddedDomainEvent(Project Project, ProjectParameter ProjectParameter)
    : IDomainEvent
{
    public Project Project { get; set; } = Project;
    public ProjectParameter ProjectParameter { get; set; } = ProjectParameter;
}
