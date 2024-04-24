using Domain;
using Domain.Projects;

namespace Application.Project.Create;

public record CreateProjectParameter
{
    public Guid PropertyId { get; set; }
    public double Value { get; set; }
}

public record CreateProjectCommand : ICommand
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string Operator { get; set; }
    public CollectorType CollectorType { get; set; }
    public ProjectType ProjectType { get; set; }
    public List<Guid> MethodIds { get; set; }
    public List<CreateProjectParameter> Parameters { get; set; }
}