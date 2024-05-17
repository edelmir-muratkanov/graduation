using Domain.Methods;

namespace Application.Method.AddParameters;

/// <summary>
/// Параметр метода.
/// </summary>
/// <param name="PropertyId">Идентификатор свойства.</param>
/// <param name="First">Группа значений первого параметра.</param>
/// <param name="Second">Группа значений второго параметра.</param>
public record MethodParameter(
    Guid PropertyId,
    ParameterValueGroup? First,
    ParameterValueGroup? Second);

/// <summary>
/// Команда для добавления параметров метода.
/// </summary>
/// <param name="MethodId">Идентификатор метода</param>
/// <param name="Parameters">Список параметров <see cref="MethodParameter"/></param>
public record AddMethodParametersCommand(
    Guid MethodId,
    List<MethodParameter> Parameters) : ICommand;
