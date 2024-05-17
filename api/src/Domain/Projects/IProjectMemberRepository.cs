namespace Domain.Projects;

/// <summary>
/// Интерфейс репозитория участников проекта.
/// </summary>
public interface IProjectMemberRepository
{
    /// <summary>
    /// Вставляет коллекцию участников проекта.
    /// </summary>
    void InsertRange(List<ProjectMember> members);

    /// <summary>
    /// Удаляет коллекцию участников проекта.
    /// </summary>
    void RemoveRange(List<ProjectMember> members);
}
