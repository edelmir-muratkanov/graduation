using Application.Abstractions.Authentication;

namespace Infrastructure.Authentication;

/// <summary>
/// Менеджер паролей.
/// </summary>
public class PasswordManager : IPasswordManager
{
    /// <inheritdoc/>
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <inheritdoc/>
    public bool VerifyPassword(string hash, string plain)
    {
        return BCrypt.Net.BCrypt.Verify(plain, hash);
    }
}
