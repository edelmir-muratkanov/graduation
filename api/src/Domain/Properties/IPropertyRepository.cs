namespace Domain.Properties;

/// <summary>
/// Представляет репозиторий для работы с сущностями свойств.
/// </summary>
public interface IPropertyRepository
{
    /// <summary>
    /// Получает свойство по его уникальному идентификатору асинхронно.
    /// </summary>
    /// <param name="id">Уникальный идентификатор свойства.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, возвращающая свойство или null, если свойство не найдено.</returns>
    Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет, является ли заданное название уникальным среди всех свойств.
    /// </summary>
    /// <param name="name">Название свойства для проверки уникальности.</param>
    /// <returns>Задача, возвращающая true, если название уникально, в противном случае - false.</returns>
    Task<bool> IsNameUniqueAsync(string name);

    /// <summary>
    /// Проверяет, существует ли свойство с заданным уникальным идентификатором.
    /// </summary>
    /// <param name="id">Уникальный идентификатор свойства.</param>
    /// <returns>Задача, возвращающая true, если свойство существует, в противном случае - false.</returns>
    Task<bool> Exists(Guid id);

    /// <summary>
    /// Вставляет новое свойство в репозиторий.
    /// </summary>
    /// <param name="property">Свойство для вставки.</param>
    void Insert(Property property);

    /// <summary>
    /// Удаляет указанное свойство из репозитория.
    /// </summary>
    /// <param name="property">Свойство для удаления.</param>
    void Remove(Property property);

    /// <summary>
    /// Обновляет информацию о свойстве в репозитории.
    /// </summary>
    /// <param name="property">Свойство для обновления.</param>
    void Update(Property property);
}
