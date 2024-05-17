namespace Application.Project.RemoveParameter;

/// <summary>
/// Команда для удаления параметра из проекта.
/// </summary>
public sealed record RemoveProjectParameterCommand : ICommand
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Идентификатор параметра.
    /// </summary>
    public Guid ParameterId { get; init; }
}
