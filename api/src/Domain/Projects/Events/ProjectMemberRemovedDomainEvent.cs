namespace Domain.Projects.Events;

/// <summary>
/// Событие удаления участника из проекта.
/// </summary>
public sealed record ProjectMemberRemovedDomainEvent(Guid ProjectId, Guid MemberId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор проекта, из которого удален участник.
    /// </summary>
    public Guid ProjectId { get; set; } = ProjectId;

    /// <summary>
    /// Идентификатор удаленного участника.
    /// </summary>
    public Guid MemberId { get; set; } = MemberId;
}
