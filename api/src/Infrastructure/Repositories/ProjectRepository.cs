using Domain.Projects;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с проектами
/// </summary>
internal sealed class ProjectRepository(ApplicationWriteDbContext context) : IProjectRepository
{
    /// <inheritdoc />
    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Projects
            .Include(p => p.Parameters)
            .Include(p => p.Methods)
            .Include(p => p.Members)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public void Insert(Project project)
    {
        context.Projects.Add(project);
    }

    /// <inheritdoc />
    public void Remove(Project project)
    {
        context.Projects.Remove(project);
    }

    /// <inheritdoc />
    public void Update(Project project)
    {
        context.Projects.Update(project);
    }
}
