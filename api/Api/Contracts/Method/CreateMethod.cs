using Api.Domain;

namespace Api.Contracts.Method;

public record CreateMethodParameterValueGroup(double Min, double Avg, double Max);

public record CreateMethodParameter(
    Guid PropertyId,
    CreateMethodParameterValueGroup? First,
    CreateMethodParameterValueGroup? Second);

public record CreateMethodRequest(
    string Name,
    HashSet<CollectorType> CollectorTypes,
    List<CreateMethodParameter> Parameters);

public record CreateMethodResponse(Guid Id, string Name);