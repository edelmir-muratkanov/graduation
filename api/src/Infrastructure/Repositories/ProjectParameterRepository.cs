using Domain.Projects;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с параметрами проекта.
/// </summary>
internal sealed class ProjectParameterRepository(ApplicationWriteDbContext context) : IProjectParameterRepository
{
    /// <inheritdoc />
    public void InsertRange(List<ProjectParameter> parameters)
    {
        context.ProjectParameters.AddRange(parameters);
    }

    /// <inheritdoc />
    public void RemoveRange(List<ProjectParameter> parameters)
    {
        context.ProjectParameters.RemoveRange(parameters);
    }
}
