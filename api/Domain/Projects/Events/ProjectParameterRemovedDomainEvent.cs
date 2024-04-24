namespace Domain.Projects.Events;

public sealed record ProjectParameterRemovedDomainEvent(Guid ProjectId, Guid ParameterId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid ParameterId { get; set; } = ParameterId;
}
