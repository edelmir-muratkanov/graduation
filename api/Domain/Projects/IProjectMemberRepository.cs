namespace Domain.Projects;

public interface IProjectMemberRepository
{
    void InsertRange(List<ProjectMember> members);
    void RemoveRange(List<ProjectMember> members);
}