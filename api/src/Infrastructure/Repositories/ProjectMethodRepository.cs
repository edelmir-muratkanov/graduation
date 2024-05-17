using Domain.Projects;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с методами проекта.
/// </summary>
internal sealed class ProjectMethodRepository(ApplicationWriteDbContext context) : IProjectMethodRepository
{
    /// <inheritdoc />
    public void InsertRange(List<ProjectMethod> methods)
    {
        context.ProjectMethods.AddRange(methods);
    }

    /// <inheritdoc />
    public void RemoveRange(List<ProjectMethod> methods)
    {
        context.ProjectMethods.RemoveRange(methods);
    }
}
