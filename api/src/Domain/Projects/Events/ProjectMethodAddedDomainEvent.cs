namespace Domain.Projects.Events;

/// <summary>
/// Событие добавления метода в проект.
/// </summary>
public sealed record ProjectMethodAddedDomainEvent(Guid ProjectId, Guid MethodId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор проекта, в который добавлен метод.
    /// </summary>
    public Guid ProjectId { get; set; } = ProjectId;

    /// <summary>
    /// Идентификатор добавленного метода.
    /// </summary>
    public Guid MethodId { get; set; } = MethodId;
}
