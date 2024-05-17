namespace Domain.Methods.Events;

/// <summary>
/// Событие доменной модели, представляющее добавление нового параметра к методу.
/// </summary>
public sealed record MethodParameterAddedDomainEvent(Guid MethodId, Guid MethodParameterId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public Guid MethodId { get; set; } = MethodId;

    /// <summary>
    /// Идентификатор добавленного параметра метода.
    /// </summary>
    public Guid MethodParameterId { get; set; } = MethodParameterId;
}
