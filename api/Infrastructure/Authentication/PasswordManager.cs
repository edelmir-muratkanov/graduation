using Application.Abstractions.Authentication;

namespace Infrastructure.Authentication;

public class PasswordManager : IPasswordManager
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hash, string plain)
    {
        return BCrypt.Net.BCrypt.Verify(plain, hash);
    }
}
