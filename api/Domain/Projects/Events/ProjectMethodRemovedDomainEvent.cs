namespace Domain.Projects.Events;

public record ProjectMethodRemovedDomainEvent(Guid ProjectId, Guid MethodId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid MethodId { get; set; } = MethodId;
}
