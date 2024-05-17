namespace Domain.Projects;

/// <summary>
/// Представляет участника проекта.
/// </summary>
/// <param name="ProjectId">Идентификатор проекта.</param>
/// <param name="MemberId">Идентификатор участника.</param>
public record ProjectMember(Guid ProjectId, Guid MemberId)
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public Guid ProjectId { get; set; } = ProjectId;

    /// <summary>
    /// Идентификатор участника.
    /// </summary>
    public Guid MemberId { get; set; } = MemberId;
}
