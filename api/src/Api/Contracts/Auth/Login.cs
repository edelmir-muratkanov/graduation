namespace Api.Contracts.Auth;

/// <summary>
/// Представляет ответ на запрос логина.
/// </summary>
/// <param name="Id">Уникальный идентификатор пользователя.</param>
/// <param name="Email">Электронная почта пользователя.</param>
/// <param name="Role">Роль пользователя.</param>
/// <param name="Token">JWT токен для аутентификации.</param>
public record LoginResponse(Guid Id, string Email, string Role, string Token);
