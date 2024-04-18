namespace Api.Shared.Interfaces;

public interface IPasswordManager
{
    string HashPassword(string password);
    bool VerifyPassword(string hash, string plain);
}