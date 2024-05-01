using Domain;
using Domain.Projects;

namespace Application.Project.GetProjectById;

public record GetProjectByIdMember
{
    public Guid Id { get; set; }
}

public record GetProjectByIdMethod
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}

public record GetProjectByIdParameter
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Unit { get; set; }
    public required double Value { get; set; }
}

public record GetProjectByIdResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Operator { get; set; }
    public required ProjectType Type { get; set; }
    public required Guid OwnerId { get; set; }
    public required CollectorType CollectorType { get; set; }
    public required List<GetProjectByIdMember> Members { get; set; }
    public required List<GetProjectByIdMethod> Methods { get; set; }
    public required List<GetProjectByIdParameter> Parameters { get; set; }
}

public record GetProjectByIdQuery : IQuery<GetProjectByIdResponse>
{
    public required Guid Id { get; init; }
}
