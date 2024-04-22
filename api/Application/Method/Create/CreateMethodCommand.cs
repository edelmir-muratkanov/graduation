using Domain;

namespace Application.Method.Create;

public record CreateMethodParameterValueGroupRequest(double Min, double Avg, double Max);

public record CreateMethodParameterRequest(
    Guid PropertyId,
    CreateMethodParameterValueGroupRequest? FirstParameters,
    CreateMethodParameterValueGroupRequest? SecondParameters);

public record CreateMethodCommand(
    string Name,
    List<CollectorType> CollectorTypes,
    List<CreateMethodParameterRequest> Parameters)
    : ICommand;