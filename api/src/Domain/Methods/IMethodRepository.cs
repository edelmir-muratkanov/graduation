namespace Domain.Methods;

/// <summary>
/// Интерфейс репозитория для работы с методами.
/// </summary>
public interface IMethodRepository
{
    /// <summary>
    /// Получает метод по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор метода.</param>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    /// <returns>Задача, представляющая асинхронную операцию получения метода.</returns>
    Task<Method?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет, уникально ли указанное имя метода.
    /// </summary>
    /// <param name="name">Имя метода для проверки.</param>
    /// <returns>Задача, представляющая асинхронную операцию проверки уникальности имени метода.</returns>
    Task<bool> IsNameUniqueAsync(string name);

    /// <summary>
    /// Вставляет новый метод.
    /// </summary>
    /// <param name="method">Метод для вставки.</param>
    void Insert(Method method);

    /// <summary>
    /// Обновляет существующий метод.
    /// </summary>
    /// <param name="method">Метод для обновления.</param>
    void Update(Method method);

    /// <summary>
    /// Удаляет существующий метод.
    /// </summary>
    /// <param name="method">Метод для удаления.</param>
    void Remove(Method method);

    /// <summary>
    /// Проверяет существование метода по его идентификатору.
    /// </summary>
    /// <param name="methodId">Идентификатор метода.</param>
    /// <returns>Задача, представляющая асинхронную операцию проверки существования метода.</returns>
    Task<bool> Exists(Guid methodId);
}
