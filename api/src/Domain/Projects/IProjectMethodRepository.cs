namespace Domain.Projects;

/// <summary>
/// Интерфейс репозитория методов проекта.
/// </summary>
public interface IProjectMethodRepository
{
    /// <summary>
    /// Вставляет коллекцию методов проекта.
    /// </summary>
    void InsertRange(List<ProjectMethod> methods);

    /// <summary>
    /// Удаляет коллекцию методов проекта.
    /// </summary>
    void RemoveRange(List<ProjectMethod> methods);
}
