namespace Domain.Projects.Events;

public sealed record ProjectParameterRemovedDomainEvent(Guid ProjectId, Guid PropertyId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid PropertyId { get; set; } = PropertyId;
}
