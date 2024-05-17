using Domain;
using Domain.Projects;

namespace Application.Project.Update;

/// <summary>
/// Команда для обновления информации о проекте.
/// </summary>
/// <param name="Id">Идентификатор проекта.</param>
/// <param name="Name">Название проекта.</param>
/// <param name="Country">Страна проекта.</param>
/// <param name="Operator">Оператор проекта.</param>
/// <param name="ProjectType">Тип проекта.</param>
/// <param name="CollectorType">Тип коллектора проекта.</param>
public record UpdateProjectCommand(
    Guid Id,
    string? Name,
    string? Country,
    string? Operator,
    ProjectType? ProjectType,
    CollectorType? CollectorType) : ICommand;
