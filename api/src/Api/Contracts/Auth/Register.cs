namespace Api.Contracts.Auth;


/// <summary>
/// Представляет запрос на регистрацию нового пользователя.
/// </summary>
/// <param name="Email">Email адрес пользователя.</param>
/// <param name="Password">Пароль пользователя.</param>
public record RegisterRequest(string Email, string Password);

/// <summary>
/// Представляет ответ на запрос регистрации нового пользователя.
/// </summary>
/// <param name="Id">Уникальный идентификатор пользователя.</param>
/// <param name="Email">Email адрес пользователя.</param>
/// <param name="Role">Роль пользователя в системе.</param>
/// <param name="Token">JWT токен для аутентификации.</param>
public record RegisterResponse(Guid Id, string Email, string Role, string Token);
