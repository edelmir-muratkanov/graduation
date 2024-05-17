using Domain;

namespace Application.Method.Create;

/// <summary>
/// Запрос на создание группы значений параметра метода.
/// </summary>
/// <param name="Min">Минимальное геолого-физическое значение.</param>
/// <param name="Avg">Геолого-физическое значение.</param>
/// <param name="Max">Максимальное геолого-физическое значение.</param>
public record CreateMethodParameterValueGroupRequest(double Min, double Avg, double Max);

/// <summary>
/// Запрос на создание параметра метода.
/// </summary>
/// <param name="PropertyId">Идентификатор свойства.</param>
/// <param name="FirstParameters">Первая группа значений параметра.</param>
/// <param name="SecondParameters">Вторая группа значений параметра.</param>
public record CreateMethodParameterRequest(
    Guid PropertyId,
    CreateMethodParameterValueGroupRequest? FirstParameters,
    CreateMethodParameterValueGroupRequest? SecondParameters);

/// <summary>
/// Команда на создание метода.
/// </summary>
/// <param name="Name">Название метода.</param>
/// <param name="CollectorTypes">Список типов коллекторов.</param>
/// <param name="Parameters">Список параметров метода.</param>
public record CreateMethodCommand(
    string Name,
    List<CollectorType> CollectorTypes,
    List<CreateMethodParameterRequest> Parameters)
    : ICommand;
