using Domain;
using Domain.Projects;

namespace Application.Project.GetUserProjects;

/// <summary>
/// Ответ на запрос <see cref="GetUserProjectsQuery"/>
/// </summary>
public record ProjectResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Operator { get; set; }
    public required ProjectType Type { get; set; }
    public required CollectorType CollectorType { get; set; }
}

/// <summary>
/// Запрос на получение проектов пользователя.
/// </summary
public record GetUserProjectsQuery : IQuery<List<ProjectResponse>>;
