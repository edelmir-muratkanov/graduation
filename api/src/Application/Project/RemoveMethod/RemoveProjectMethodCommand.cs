namespace Application.Project.RemoveMethod;

/// <summary>
/// Команда для удаления метода из проекта.
/// </summary>
public sealed record RemoveProjectMethodCommand : ICommand
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public required Guid ProjectId { get; set; }
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public required Guid MethodId { get; set; }
}
