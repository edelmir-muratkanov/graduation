namespace Domain.Projects.Events;

public record ProjectCreatedDomainEvent(Guid ProjectId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
}
