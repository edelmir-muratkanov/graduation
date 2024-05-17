namespace Domain.Projects.Events;

/// <summary>
/// Событие добавления участника в проект.
/// </summary>
public sealed record ProjectMemberAddedDomainEvent(Guid ProjectId, Guid MemberId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор проекта, к которому добавлен участник.
    /// </summary>
    public Guid ProjectId { get; set; } = ProjectId;

    /// <summary>
    /// Идентификатор добавленного участника.
    /// </summary>
    public Guid MemberId { get; set; } = MemberId;
}
