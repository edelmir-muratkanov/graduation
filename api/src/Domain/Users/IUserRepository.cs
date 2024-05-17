namespace Domain.Users;

/// <summary>
/// Представляет интерфейс репозитория пользователей.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получает пользователя по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    /// <returns>Задача, представляющая операцию поиска пользователя.</returns>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает пользователя по его адресу электронной почты.
    /// </summary>
    /// <param name="email">Адрес электронной почты пользователя.</param>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    /// <returns>Задача, представляющая операцию поиска пользователя.</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет уникальность адреса электронной почты.
    /// </summary>
    /// <param name="email">Адрес электронной почты, который требуется проверить на уникальность.</param>
    /// <returns>Задача, представляющая операцию проверки уникальности адреса электронной почты.</returns>
    Task<bool> IsEmailUniqueAsync(string email);

    /// <summary>
    /// Вставляет нового пользователя в репозиторий.
    /// </summary>
    /// <param name="user">Пользователь для вставки.</param>
    void Insert(User user);
}
