namespace Domain.Projects.Events;

/// <summary>
/// Событие добавления параметра в проект.
/// </summary>
public sealed record ProjectParameterAddedDomainEvent(Guid ProjectId, Guid ProjectParameterId)
    : IDomainEvent
{
    /// <summary>
    /// Идентификатор проекта, в который добавлен параметр.
    /// </summary>
    public Guid ProjectId { get; set; } = ProjectId;

    /// <summary>
    /// Идентификатор добавленного параметра.
    /// </summary>
    public Guid ProjectParameterId { get; set; } = ProjectParameterId;
}
