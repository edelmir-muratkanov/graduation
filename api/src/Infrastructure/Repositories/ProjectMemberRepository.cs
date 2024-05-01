using Domain.Projects;

namespace Infrastructure.Repositories;

internal sealed class ProjectMemberRepository(ApplicationWriteDbContext context) : IProjectMemberRepository
{
    public void InsertRange(List<ProjectMember> members)
    {
        context.ProjectMembers.AddRange(members);
    }

    public void RemoveRange(List<ProjectMember> members)
    {
        context.ProjectMembers.RemoveRange(members);
    }
}
