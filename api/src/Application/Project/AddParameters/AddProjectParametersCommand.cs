namespace Application.Project.AddParameters;

/// <summary>
/// Параметр для добавления в проект.
/// </summary>
public record AddProjectParameter
{
    /// <summary>
    /// Идентификатор свойства.
    /// </summary>
    public required Guid PropertyId { get; init; }
    /// <summary>
    /// Значение параметра.
    /// </summary>
    public required double Value { get; init; }
}

/// <summary>
/// Команда для добавления параметров проекта.
/// </summary>
public record AddProjectParametersCommand : ICommand
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public required Guid ProjectId { get; init; }

    /// <summary>
    /// Список параметров проекта.
    /// </summary>
    public required List<AddProjectParameter> Parameters { get; init; }
}
