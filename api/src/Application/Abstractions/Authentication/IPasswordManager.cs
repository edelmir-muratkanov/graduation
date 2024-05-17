namespace Application.Abstractions.Authentication;

/// <summary>
/// Предоставляет интерфейс для хеширования и проверки паролей.
/// </summary>
public interface IPasswordManager
{
    /// <summary>
    /// Хеширует пароль.
    /// </summary>
    /// <param name="password">Пароль для хеширования.</param>
    /// <returns>Хеш пароля.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Проверяет соответствие хеша паролю.
    /// </summary>
    /// <param name="hash">Хеш пароля.</param>
    /// <param name="plain">Входной пароль для проверки.</param>
    /// <returns>True, если пароль верен, иначе - False.</returns>
    bool VerifyPassword(string hash, string plain);
}
