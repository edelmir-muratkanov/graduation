namespace Application.Project.Remove;

/// <summary>
/// Команда на удаление проекта.
/// </summary>
public record RemoveProjectCommand : ICommand
{
    /// <summary>
    /// Идентификатор проекта для удаления.
    /// </summary>
    public required Guid ProjectId { get; init; }
}
