using Domain;
using Domain.Projects;

namespace Application.Project.Create;

/// <summary>
/// Команда для создания проекта.
/// </summary>
public record CreateProjectCommand : ICommand<Guid>
{
    /// <summary>
    /// Название проекта.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Страна, в которой осуществляется проект.
    /// </summary>
    public string Country { get; init; }

    /// <summary>
    /// Оператор проекта.
    /// </summary>
    public string Operator { get; init; }

    /// <summary>
    /// Тип коллектора проекта.
    /// </summary>
    public CollectorType CollectorType { get; init; }

    /// <summary>
    /// Тип проекта.
    /// </summary>
    public ProjectType ProjectType { get; init; }
}
