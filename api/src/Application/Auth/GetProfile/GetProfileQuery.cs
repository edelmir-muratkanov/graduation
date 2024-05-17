namespace Application.Auth.GetProfile;

/// <summary>
/// Контракт для ответа на апрос <see cref="GetProfileQuery"/>
/// </summary>
/// <param name="Id">Идентификатор пользователя</param>
/// <param name="Email">Адрес электронной почты пользователя</param>
/// <param name="Role">Роль пользователя</param>
public record GetProfileResponse(string Id, string Email, string Role);

/// <summary>
/// Запрос на получение профиля пользователя.
/// </summary>
public sealed record GetProfileQuery : IQuery<GetProfileResponse>;
