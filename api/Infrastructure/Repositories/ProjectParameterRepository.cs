using Domain.Projects;

namespace Infrastructure.Repositories;

internal sealed class ProjectParameterRepository(ApplicationWriteDbContext context) : IProjectParameterRepository
{
    public void InsertRange(List<ProjectParameter> parameters)
    {
        context.ProjectParameters.AddRange(parameters);
    }

    public void RemoveRange(List<ProjectParameter> parameters)
    {
        context.ProjectParameters.RemoveRange(parameters);
    }
}
