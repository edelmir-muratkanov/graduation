namespace Application.Method.Delete;

/// <summary>
/// Команда для удаления метода.
/// </summary>
public record DeleteMethodCommand : ICommand
{
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public Guid Id { get; init; }
}
