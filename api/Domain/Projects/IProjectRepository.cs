namespace Domain.Projects;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Project project);
    void Remove(Project project);
    void Update(Project project);
}
