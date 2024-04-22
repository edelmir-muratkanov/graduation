namespace Domain.Projects;

public interface IProjectMethodRepository
{
    void InsertRange(List<ProjectMethod> methods);
    void RemoveRange(List<ProjectMethod> methods);
}