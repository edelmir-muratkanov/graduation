namespace Domain.Projects.Events;

public sealed record ProjectBaseInfoUpdatedDomainEvent(Guid ProjectId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
}
