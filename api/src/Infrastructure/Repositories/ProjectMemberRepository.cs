using Domain.Projects;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с участниками проекта.
/// </summary>
internal sealed class ProjectMemberRepository(ApplicationWriteDbContext context) : IProjectMemberRepository
{
    /// <inheritdoc />
    public void InsertRange(List<ProjectMember> members)
    {
        context.ProjectMembers.AddRange(members);
    }

    /// <inheritdoc />
    public void RemoveRange(List<ProjectMember> members)
    {
        context.ProjectMembers.RemoveRange(members);
    }
}
