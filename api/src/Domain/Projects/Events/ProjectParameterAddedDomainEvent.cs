namespace Domain.Projects.Events;

public sealed record ProjectParameterAddedDomainEvent(Guid ProjectId, Guid ProjectParameterId)
    : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid ProjectParameterId { get; set; } = ProjectParameterId;
}
