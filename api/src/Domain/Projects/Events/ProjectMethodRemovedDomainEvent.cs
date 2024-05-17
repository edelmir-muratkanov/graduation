namespace Domain.Projects.Events;

/// <summary>
/// Событие удаления метода из проекта.
/// </summary>
public sealed record ProjectMethodRemovedDomainEvent(Guid ProjectId, Guid MethodId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор проекта, из которого удален метод.
    /// </summary>
    public Guid ProjectId { get; set; } = ProjectId;

    /// <summary>
    /// Идентификатор удаленного метода.
    /// </summary>
    public Guid MethodId { get; set; } = MethodId;
}
