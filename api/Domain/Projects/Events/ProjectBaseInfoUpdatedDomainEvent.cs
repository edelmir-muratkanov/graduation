namespace Domain.Projects.Events;

public record ProjectBaseInfoUpdatedDomainEvent(Guid ProjectId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
}
