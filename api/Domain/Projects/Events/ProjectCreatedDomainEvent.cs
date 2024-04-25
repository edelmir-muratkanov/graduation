namespace Domain.Projects.Events;

public sealed record ProjectCreatedDomainEvent(Project Project) : IDomainEvent
{
    public Project Project { get; set; } = Project;
}
