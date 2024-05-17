namespace Application.Project.AddMethods;

/// <summary>
/// Команда для добавления методов в проект.
/// </summary>
public record AddProjectMethodsCommand : ICommand
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public required Guid ProjectId { get; init; }
    /// <summary>
    /// Идентификаторы методов.
    /// </summary>
    public required List<Guid> MethodIds { get; init; }
}
