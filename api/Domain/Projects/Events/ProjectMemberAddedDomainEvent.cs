namespace Domain.Projects.Events;

public sealed record ProjectMemberAddedDomainEvent : IDomainEvent
{
    public Guid ProjectId { get; set; }
    public Guid MemberId { get; set; }
}
