namespace Infrastructure.Authentication;

public class JwtOptions
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int AccessExpiryInMinutes { get; init; }
    public int RefreshExpiryInDays { get; init; }
}
