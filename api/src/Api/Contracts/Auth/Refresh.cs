namespace Api.Contracts.Auth;

/// <summary>
/// Представляет ответ на запрос обновления токена.
/// </summary>
/// <param name="Token">Новый JWT токен для аутентификации.</param>
public record RefreshResponse(string Token);
