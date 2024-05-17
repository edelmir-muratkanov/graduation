namespace Domain.Projects.Events;

/// <summary>
/// Событие обновления базовой информации о проекте.
/// </summary>
public sealed record ProjectBaseInfoUpdatedDomainEvent(Guid ProjectId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор проекта, для которого обновлена базовая информация.
    /// </summary>
    public Guid ProjectId { get; set; } = ProjectId;
}
