using Domain;
using Domain.Projects;

namespace Application.Project.Update;

public record UpdateProjectCommand(
    Guid Id,
    string? Name,
    string? Country,
    string? Operator,
    ProjectType? ProjectType,
    CollectorType? CollectorType) : ICommand;
