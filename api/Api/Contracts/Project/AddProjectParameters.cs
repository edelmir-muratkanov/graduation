namespace Api.Contracts.Project;

public record AddProjectParametersRequest
{
    public required Guid PropertyId { get; set; }
    public required double Value { get; set; }
}