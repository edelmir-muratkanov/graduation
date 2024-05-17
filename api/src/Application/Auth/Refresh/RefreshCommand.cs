namespace Application.Auth.Refresh;

/// <summary>
/// Контракт для ответа на комманду <see cref="RefreshCommand"/>
/// </summary>
/// <param name="AccessToken">Access токен</param>
/// <param name="RefreshToken">Refresh токен</param>
public record RefreshResponse(string AccessToken, string RefreshToken);

/// <summary>
/// Команда для обновления JWT-токенов.
/// </summary>
/// <param name="AccessToken">Access токен</param>
/// <param name="RefreshToken">Refresh токен</param>
public record RefreshCommand(string AccessToken, string RefreshToken) : ICommand<RefreshResponse>;
