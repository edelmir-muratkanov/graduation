namespace Domain.Users.Events;

/// <summary>
/// Представляет событие регистрации пользователя.
/// </summary>
/// <param name="userId">Идентификатор зарегистрированного пользователя.</param>
public sealed class UserRegisteredDomainEvent(Guid userId) : IDomainEvent
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; init; } = userId;
}
