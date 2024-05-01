using Domain;

namespace Api.Contracts.Method;

public record CreateMethodParameterValueGroup
{
    public required double Max { get; set; }
    public required double Min { get; set; }
    public required double Avg { get; set; }
}

public record CreateMethodParameter
{
    public Guid PropertyId { get; set; }
    public CreateMethodParameterValueGroup? First { get; set; }
    public CreateMethodParameterValueGroup? Second { get; set; }
}

public record CreateMethodRequest
{
    public required string Name { get; set; }
    public required List<CollectorType> CollectorTypes { get; set; }
    public required List<CreateMethodParameter> Parameters { get; set; }
}
