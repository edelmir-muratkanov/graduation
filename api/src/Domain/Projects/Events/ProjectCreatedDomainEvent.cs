namespace Domain.Projects.Events;

/// <summary>
/// Событие создания проекта.
/// </summary>
public sealed record ProjectCreatedDomainEvent(Guid ProjectId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор созданного проекта.
    /// </summary>
    public Guid ProjectId { get; set; } = ProjectId;
}
