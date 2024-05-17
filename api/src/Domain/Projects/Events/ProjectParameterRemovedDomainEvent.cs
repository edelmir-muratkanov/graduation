namespace Domain.Projects.Events;

/// <summary>
/// Событие удаления параметра из проекта.
/// </summary>
public sealed record ProjectParameterRemovedDomainEvent(Guid ProjectId, Guid PropertyId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор проекта, из которого удален параметр.
    /// </summary>
    public Guid ProjectId { get; set; } = ProjectId;

    /// <summary>
    /// Идентификатор удаленного параметра.
    /// </summary>
    public Guid PropertyId { get; set; } = PropertyId;
}
