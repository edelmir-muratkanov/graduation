namespace Domain.Methods.Events;

/// <summary>
/// Событие доменной модели, которое представляет обновление метода.
/// </summary>
public sealed record MethodUpdatedDomainEvent(Guid MethodId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public Guid MethodId { get; set; } = MethodId;
}
