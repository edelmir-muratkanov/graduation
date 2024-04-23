using Domain.Projects;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class ProjectRepository(ApplicationWriteDbContext context) : IProjectRepository
{
    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Projects.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void Insert(Project project)
    {
        context.Projects.Add(project);
    }

    public void Remove(Project project)
    {
        context.Projects.Remove(project);
    }

    public void Update(Project project)
    {
        context.Projects.Update(project);
    }
}