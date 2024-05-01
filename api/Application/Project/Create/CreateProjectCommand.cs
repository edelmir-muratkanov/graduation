using Domain;
using Domain.Projects;

namespace Application.Project.Create;


public record CreateProjectCommand : ICommand<Guid>
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string Operator { get; set; }
    public CollectorType CollectorType { get; set; }
    public ProjectType ProjectType { get; set; }
}
