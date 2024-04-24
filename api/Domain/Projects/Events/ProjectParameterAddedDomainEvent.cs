namespace Domain.Projects.Events;

public sealed record ProjectParameterAddedDomainEvent(Guid ProjectParameterId) : IDomainEvent
{
    public Guid ProjectParameterId { get; set; } = ProjectParameterId;
}
