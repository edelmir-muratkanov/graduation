using Domain;
using Domain.Projects;

namespace Api.Contracts.Project;

public record CreateProjectParameter
{
    public Guid PropertyId { get; set; }
    public double Value { get; set; }
}

public record CreateProjectRequest
{
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Operator { get; set; }
    public required ProjectType Type { get; set; }
    public required CollectorType CollectorType { get; set; }
    public required List<Guid> MethodIds { get; set; }
    public required List<CreateProjectParameter> Parameters { get; set; }
}
