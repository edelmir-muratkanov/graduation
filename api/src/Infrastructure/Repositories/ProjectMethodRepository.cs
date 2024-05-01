using Domain.Projects;

namespace Infrastructure.Repositories;

internal sealed class ProjectMethodRepository(ApplicationWriteDbContext context) : IProjectMethodRepository
{
    public void InsertRange(List<ProjectMethod> methods)
    {
        context.ProjectMethods.AddRange(methods);
    }

    public void RemoveRange(List<ProjectMethod> methods)
    {
        context.ProjectMethods.RemoveRange(methods);
    }
}
