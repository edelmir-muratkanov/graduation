using Domain.Methods;
using Domain.Projects;

namespace Domain.Calculation;

public interface ICalculationService
{
    Task<Result> Create(Project project, Method method);
    Task<Result> Update(Project project, Method method);
    Belonging CalculateBelongingDegree(
        double? nullableProjectValue,
        ParameterValueGroup? methodFirst,
        ParameterValueGroup? methodSecond);
}
