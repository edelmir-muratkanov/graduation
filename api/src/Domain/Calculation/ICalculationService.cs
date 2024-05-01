using Domain.Methods;
using Domain.Projects;

namespace Domain.Calculation;

public interface ICalculationService
{
    Belonging CalculateBelongingDegree(
        double? nullableProjectValue,
        ParameterValueGroup? methodFirst,
        ParameterValueGroup? methodSecond);
}
