namespace Domain.Projects.Events;

public sealed record ProjectMemberRemovedDomainEvent(Project Project, ProjectMember ProjectMember) : IDomainEvent
{
    public Project Project { get; set; } = Project;
    public ProjectMember ProjectMember { get; set; } = ProjectMember;
}
