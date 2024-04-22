namespace Domain.Projects.Events;

public record ProjectParameterRemovedDomainEvent(Guid ProjectId, Guid ParameterId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid ParameterId { get; set; } = ParameterId;
}