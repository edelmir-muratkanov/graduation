namespace Application.Project.RemoveMember;

public class RemoveProjectMemberCommand : ICommand
{
    public required Guid ProjectId { get; init; }
    public required Guid MemberId { get; init; }
}
