namespace Domain.Projects.Events;

public sealed record ProjectBaseInfoUpdatedDomainEvent(Project Project) : IDomainEvent
{
    public Project Project { get; set; } = Project;
}
