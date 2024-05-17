namespace Domain.Projects;

/// <summary>
/// Интерфейс репозитория проектов.
/// </summary>
public interface IProjectRepository
{
    /// <summary>
    /// Асинхронно получает проект по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор проекта.</param>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    /// <returns>Задача, представляющая получение проекта.</returns>
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Вставляет проект.
    /// </summary>
    /// <param name="project">Проект для вставки.</param>
    void Insert(Project project);

    /// <summary>
    /// Удаляет проект.
    /// </summary>
    /// <param name="project">Проект для удаления.</param>
    void Remove(Project project);

    /// <summary>
    /// Обновляет проект.
    /// </summary>
    /// <param name="project">Проект для обновления.</param>
    void Update(Project project);
}
