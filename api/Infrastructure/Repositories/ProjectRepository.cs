using Domain.Projects;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class ProjectRepository(ApplicationWriteDbContext context) : IProjectRepository
{
    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Projects
            .Include(p => p.Parameters)
            .Include(p => p.Methods)
            .Include(p => p.Members)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
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
