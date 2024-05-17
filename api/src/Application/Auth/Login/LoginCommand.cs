namespace Application.Auth.Login;

/// <summary>
/// Контракт для ответа на комманду <see cref="LoginCommand"/>
/// </summary>
/// <param name="Id">Идентификатор пользователя</param>
/// <param name="Email">Адрес электронной почты пользователя</param>
/// <param name="Role">Роль пользователя</param>
/// <param name="AccessToken">Access токен для подтверждения пользователя</param>
/// <param name="RefreshToken">Refresh токен пользователя</param>
public record LoginResponse(Guid Id, string Email, string Role, string AccessToken, string RefreshToken);

/// <summary>
/// Команда для входа в систему.
/// </summary>
/// <param name="Email">Адрес электронной почты</param>
/// <param name="Password">Пароль</param>
public sealed record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;
