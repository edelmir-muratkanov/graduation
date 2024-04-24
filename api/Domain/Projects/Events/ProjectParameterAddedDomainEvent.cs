namespace Domain.Projects.Events;

public record ProjectParameterAddedDomainEvent(Guid ProjectParameterId) : IDomainEvent
{
    public Guid ProjectParameterId { get; set; } = ProjectParameterId;
}
