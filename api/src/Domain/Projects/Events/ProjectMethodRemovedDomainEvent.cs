namespace Domain.Projects.Events;

public sealed record ProjectMethodRemovedDomainEvent(Guid ProjectId, Guid MethodId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid MethodId { get; set; } = MethodId;
}
