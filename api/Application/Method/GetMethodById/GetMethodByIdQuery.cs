using Domain;
using Domain.Methods;

namespace Application.Method.GetMethodById;

public record GetMethodByIdParameterResponse
{
    public required Guid Id { get; set; }
    public required string PropertyName { get; set; }
    public required string PropertyUnit { get; set; }
    public ParameterValueGroup? First { get; set; }
    public ParameterValueGroup? Second { get; set; }
}

public record GetMethodByIdResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<CollectorType> CollectorTypes { get; set; }
    public required List<GetMethodByIdParameterResponse> Parameters { get; set; }
}

public record GetMethodByIdQuery : IQuery<GetMethodByIdResponse>
{
    public required Guid Id { get; init; }
}
