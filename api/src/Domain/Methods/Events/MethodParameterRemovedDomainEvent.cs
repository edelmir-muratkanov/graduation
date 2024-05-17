namespace Domain.Methods.Events;

/// <summary>
/// Событие доменной модели, представляющее удаление параметра из метода.
/// </summary>
public sealed record MethodParameterRemovedDomainEvent(Guid MethodId, Guid PropertyId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public Guid MethodId { get; set; } = MethodId;

    /// <summary>
    /// Идентификатор свойства для удаленного параметра метода.
    /// </summary>
    public Guid PropertyId { get; set; } = PropertyId;
}
