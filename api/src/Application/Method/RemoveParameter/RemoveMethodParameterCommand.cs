namespace Application.Method.RemoveParameter;

/// <summary>
/// Команда для удаления параметра метода.
/// </summary>
public record RemoveMethodParameterCommand : ICommand
{
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public Guid MethodId { get; init; }

    /// <summary>
    /// Идентификатор параметра.
    /// </summary>
    public Guid ParameterId { get; init; }
}
