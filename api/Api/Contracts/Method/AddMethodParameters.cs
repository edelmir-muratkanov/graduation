namespace Api.Contracts.Method;

public record ValueGroup
{
    public required double Min { get; init; }
    public required double Avg { get; init; }
    public required double Max { get; init; }
}

public record AddMethodParametersRequest
{
    public required Guid PropertyId { get; init; }
    public ValueGroup? First { get; init; }
    public ValueGroup? Second { get; init; }
}