namespace Application.Project.AddMembers;

/// <summary>
/// Команда для добавления участников в проект.
/// </summary>
public sealed record AddProjectMembersCommand : ICommand
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public required Guid ProjectId { get; set; }
    /// <summary>
    /// Идентификаторы участников, которых нужно добавить в проект.
    /// </summary>
    public required List<Guid> MemberIds { get; set; }
}
