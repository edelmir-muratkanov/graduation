namespace Domain.Projects.Events;

public sealed record ProjectMethodRemovedDomainEvent(Project Project, ProjectMethod ProjectMethod) : IDomainEvent
{
    public Project Project { get; set; } = Project;
    public ProjectMethod ProjectMethod { get; set; } = ProjectMethod;
}
