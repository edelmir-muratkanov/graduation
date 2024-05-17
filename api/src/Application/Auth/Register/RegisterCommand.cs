namespace Application.Auth.Register;

/// <summary>
/// Контракт для ответа на команду <see cref="RegisterCommand"/>
/// </summary>
/// <param name="Id">Идентификатор пользователя.</param>
/// <param name="Email">Адрес электронной почты пользователя.</param>
/// <param name="Role">Роль пользователя.</param>
/// <param name="AccessToken">Access токен для подтверждения пользователя.</param>
/// <param name="RefreshToken">Refresh токен пользователя.</param>
public record RegisterResponse(Guid Id, string Email, string Role, string AccessToken, string RefreshToken);

/// <summary>
/// Команда для регистрации нового пользователя.
/// </summary>
public sealed record RegisterCommand(string Email, string Password) : ICommand<RegisterResponse>;
