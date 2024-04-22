namespace Domain.Projects.Events;

public record ProjectMemberAddedDomainEvent : IDomainEvent
{
    public Guid ProjectId { get; set; }
    public Guid MemberId { get; set; }
}