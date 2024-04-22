namespace Domain.Projects.Events;

public record ProjectMemberRemovedDomainEvent : IDomainEvent
{
    public Guid ProjectId { get; set; }
    public Guid MemberId { get; set; }
}