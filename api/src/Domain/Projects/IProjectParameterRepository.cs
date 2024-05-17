namespace Domain.Projects;

/// <summary>
/// Интерфейс репозитория параметров проекта.
/// </summary>
public interface IProjectParameterRepository
{
    /// <summary>
    /// Вставляет коллекцию параметров проекта.
    /// </summary>
    void InsertRange(List<ProjectParameter> parameters);

    /// <summary>
    /// Удаляет коллекцию параметров проекта.
    /// </summary>
    void RemoveRange(List<ProjectParameter> parameters);
}
