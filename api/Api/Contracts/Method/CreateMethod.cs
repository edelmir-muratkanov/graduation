using Api.Domain;

namespace Api.Contracts.Method;

public record CreateMethodParameterValueGroup
{
    public required double Max { get; init; }
    public required double Min { get; init; }
    public required double Avg { get; init; }
}

public record CreateMethodParameter
{
    public Guid PropertyId { get; set; }
    public CreateMethodParameterValueGroup? First { get; set; }
    public CreateMethodParameterValueGroup? Second { get; set; }
}

public record CreateMethodRequest
{
    public required string Name { get; init; }
    public required List<CollectorType> CollectorTypes { get; init; }
    public required List<CreateMethodParameter> Parameters { get; init; }
}
