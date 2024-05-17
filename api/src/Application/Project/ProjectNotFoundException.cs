namespace Application.Project;

/// <summary>
/// Исключение, возникающее, когда проект не найден.
/// </summary>
/// <param name="projectId">Идентификатор проекта, который не был найден.</param>
public sealed class ProjectNotFoundException(Guid projectId)
    : Exception($"The project with id = {projectId} not found")
{
    /// <summary>
    /// Получает идентификатор проекта, который не был найден.
    /// </summary>
    public Guid ProjectId { get; } = projectId;
}
