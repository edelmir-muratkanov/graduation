namespace Application.Calculations;

/// <summary>
/// Исключение, указывающее на то, что расчет для указанного проекта и метода не найден.
/// </summary>
/// <param name="projectId">Идентификатор проекта.</param>
/// <param name="methodId">Идентификатор метода.</param>
public sealed class CalculationNotFoundException(Guid projectId, Guid methodId)
    : Exception($"Calculation for project with id = {projectId} and method with id = {methodId} not found")
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public Guid ProjectId { get; init; } = projectId;

    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public Guid MethodId { get; init; } = methodId;
}
