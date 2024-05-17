namespace Application.Abstractions.Authentication;

/// <summary>
/// Предоставляет интерфейс для получения информации о текущем пользователе.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Возвращает идентификатор текущего пользователя.
    /// </summary>
    string? Id { get; }

    /// <summary>
    /// Возвращает роль текущего пользователя.
    /// </summary>
    string? Role { get; }

    /// <summary>
    /// Возвращает адрес электронной почты текущего пользователя.
    /// </summary>
    string? Email { get; }
}
