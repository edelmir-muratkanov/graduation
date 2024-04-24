using Domain.Methods;

namespace Application.Method.AddParameters;

public record MethodParameter(
    Guid PropertyId,
    ParameterValueGroup? First,
    ParameterValueGroup? Second);

public record AddMethodParametersCommand(
    Guid MethodId,
    List<MethodParameter> Parameters) : ICommand;
