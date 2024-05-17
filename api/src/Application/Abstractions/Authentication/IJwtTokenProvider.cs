using Domain.Users;

namespace Application.Abstractions.Authentication;

/// <summary>
/// Предоставляет интерфейс для работы с JWT-токенами.
/// </summary>
public interface IJwtTokenProvider
{
    /// <summary>
    /// Генерирует access-токен на основе пользователя.
    /// </summary>
    /// <param name="user">Пользователь, для которого генерируется токен.</param>
    /// <returns>Сгенерированный access-токен.</returns>
    string Generate(User user);

    /// <summary>
    /// Генерирует refresh-токен.
    /// </summary>
    /// <returns>Сгенерированный refresh-токен.</returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Получает пользователя из access-токена.
    /// </summary>
    /// <param name="token">access-токен.</param>
    /// <returns>Идентификатор пользователя, извлеченный из токена.</returns>
    Task<string?> GetUserFromToken(string token);
}
