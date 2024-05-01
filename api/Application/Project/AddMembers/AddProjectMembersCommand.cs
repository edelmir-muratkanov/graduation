namespace Application.Project.AddMembers;

public sealed record AddProjectMembersCommand : ICommand
{
    public required Guid ProjectId { get; set; }
    public required List<Guid> MemberIds { get; set; }
}
