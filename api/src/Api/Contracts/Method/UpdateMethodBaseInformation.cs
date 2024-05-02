using Domain;

namespace Api.Contracts.Method;

public record UpdateMethodBaseInformationRequest
{
    public string? Name { get; init; }
    public List<CollectorType>? CollectorTypes { get; init; }
}
