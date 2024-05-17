namespace Application.Project.RemoveMember;

/// <summary>
/// Команда для удаления участника из проекта.
/// </summary>
public class RemoveProjectMemberCommand : ICommand
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public required Guid ProjectId { get; init; }

    /// <summary>
    /// Идентификатор участника.
    /// </summary>
    public required Guid MemberId { get; init; }
}
