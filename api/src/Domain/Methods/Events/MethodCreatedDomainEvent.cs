namespace Domain.Methods.Events;

/// <summary>
/// Событие доменной модели, которое представляет создание нового метода.
/// </summary>
public sealed record MethodCreatedDomainEvent(Guid MethodId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public Guid MethodId { get; set; } = MethodId;
}
