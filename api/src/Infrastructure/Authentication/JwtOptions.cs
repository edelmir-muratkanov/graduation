namespace Infrastructure.Authentication;

/// <summary>
/// Опции JWT-токенов.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Наименование секции в конфигурации, содержащей настройки JWT.
    /// </summary>
    public const string SectionName = "JwtSettings";

    /// <summary>
    /// Секрет для создания и проверки JWT-токенов.
    /// </summary>
    public string Secret { get; init; } = string.Empty;

    /// <summary>
    /// Издатель (Issuer) JWT-токенов.
    /// </summary>
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// Получатель (Audience) JWT-токенов.
    /// </summary>
    public string Audience { get; init; } = string.Empty;

    /// <summary>
    /// Время жизни access-токена в минутах.
    /// </summary>
    public int AccessExpiryInMinutes { get; init; }

    /// <summary>
    /// Время жизни refresh-токена в днях.
    /// </summary>
    public int RefreshExpiryInDays { get; init; }
}
