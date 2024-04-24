using Domain;

namespace Api.Contracts.Method;

public class UpdateMethodRequest
{
    public string? Name { get; init; }
    public List<CollectorType>? CollectorTypes { get; init; }
}
