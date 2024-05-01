namespace Domain.Projects;

public interface IProjectParameterRepository
{
    void InsertRange(List<ProjectParameter> parameters);
    void RemoveRange(List<ProjectParameter> parameters);
}
