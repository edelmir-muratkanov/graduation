namespace Domain.Projects.Events;

public sealed record ProjectMemberRemovedDomainEvent(Guid ProjectId, Guid MemberId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid MemberId { get; set; } = MemberId;
}
