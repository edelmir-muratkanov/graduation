using Domain.Users.Events;

namespace Domain.Users;

/// <summary>
/// Представляет сущность пользователя.
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="User"/>.
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя.</param>
    /// <param name="email">Электронная почта пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <param name="role">Роль пользователя.</param>
    private User(Guid id, string email, string password, Role role)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
    }

    /// <summary>
    /// Закрытый конструктор без параметров, предотвращающий создание экземпляров класса за его пределами.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    private User()
    {
    }

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public Role Role { get; private set; }

    /// <summary>
    /// Refresh токен пользователя.
    /// </summary>
    public string? Token { get; private set; }

    /// <summary>
    /// Создает нового пользователя.
    /// </summary>
    /// <param name="email">Электронная почта пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <param name="role">Роль пользователя.</param>
    /// <returns>Результат операции, содержащий созданного пользователя в случае успеха, в противном случае — результат ошибки.</returns>
    public static Result<User> Create(string email, string password, Role role = Role.User)
    {
        var user = new User(Guid.NewGuid(), email, password, role);

        user.Raise(new UserRegisteredDomainEvent(user.Id));
        return user;
    }

    /// <summary>
    /// Обновляет refresh токен пользователя.
    /// </summary>
    /// <param name="token">Новое значение refresh токена.</param>
    /// <returns>Результат операции, указывающий на успешность операции.</returns>
    public Result UpdateToken(string token)
    {
        Token = token;

        return Result.Success();
    }
}
