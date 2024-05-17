namespace Domain.Projects;

/// <summary>
/// Представляет метод, связанный с проектом.
/// </summary>
/// <param name="ProjectId">Идентификатор проекта.</param>
/// <param name="MethodId">Идентификатор метода.</param>
public record ProjectMethod(Guid ProjectId, Guid MethodId)
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public Guid ProjectId { get; init; } = ProjectId;

    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public Guid MethodId { get; init; } = MethodId;
}
