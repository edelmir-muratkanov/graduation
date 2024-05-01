namespace Domain.Projects.Events;

public sealed record ProjectMemberAddedDomainEvent(Guid ProjectId, Guid MemberId) : IDomainEvent
{
    public Guid Project { get; set; } = ProjectId;
    public Guid MemberId { get; set; } = MemberId;
}
