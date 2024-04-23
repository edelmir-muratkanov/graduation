using Domain;
using Domain.Projects;

namespace Application.Project.GetProjectById;

public record GetProjectByIdMember
{
    public Guid Id { get; set; }
}

public record GetProjectByIdMethod
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public record GetProjectByIdParameter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
}

public record GetProjectByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Operator { get; set; }
    public ProjectType Type { get; set; }
    public CollectorType CollectorType { get; set; }
    public List<GetProjectByIdMember> Members { get; set; }
    public List<GetProjectByIdMethod> Methods { get; set; }
    public List<GetProjectByIdParameter> Parameters { get; set; }
}

public record GetProjectByIdQuery : IQuery<GetProjectByIdResponse>
{
    public Guid Id { get; set; }
}