namespace Application.Calculations;

public sealed class CalculationNotFoundException(Guid projectId, Guid methodId)
    : Exception($"Calculation for project with id = {projectId} and method with id = {methodId} not found")
{
    public Guid ProjectId { get; set; } = projectId;
    public Guid MethodId { get; set; } = methodId;
}
