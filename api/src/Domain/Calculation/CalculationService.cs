using Domain.Methods;
using Domain.Projects;
using Domain.Properties;

namespace Domain.Calculation;

public sealed class CalculationService(
    IPropertyRepository propertyRepository,
    ICalculationRepository calculationRepository)
    : ICalculationService
{
    public async Task<Result> Create(Project project, Method method)
    {
        var calculation = Calculation.Create(project.Id, method.Id);

        var collectorTypeBelonging = new Belonging(method.CollectorTypes.Contains(project.CollectorType) ? 1 : -1);
        calculation.AddItem("Тип коллектора", collectorTypeBelonging);

        foreach (MethodParameter methodParameter in method.Parameters)
        {
            Property property = await propertyRepository.GetByIdAsync(methodParameter.PropertyId);

            double? projectParameterValue = project.Parameters
                .FirstOrDefault(p => p.PropertyId == methodParameter.PropertyId)?.Value;

            Belonging belonging = CalculateBelongingDegree(
                projectParameterValue,
                methodParameter.FirstParameters,
                methodParameter.SecondParameters);

            calculation.AddItem(property!.Name, belonging);
        }

        calculationRepository.Insert(calculation);
        return Result.Success();
    }

    public async Task<Result> Update(Project project, Method method)
    {
        Calculation? calculation = await calculationRepository.GetOne(calculation =>
            calculation.ProjectId == project.Id &&
            calculation.MethodId == method.Id);

        if (calculation is null)
        {
            return Result.Failure(CalculationErrors.NotFound);
        }

        var collectorTypeBelonging = new Belonging(method.CollectorTypes.Contains(project.CollectorType) ? 1 : -1);

        calculation.UpdateItem("Тип коллектора", collectorTypeBelonging);

        foreach (MethodParameter methodParameter in method!.Parameters)
        {
            Property property = await propertyRepository.GetByIdAsync(methodParameter.PropertyId);

            double? projectParameterValue = project!.Parameters
                .FirstOrDefault(p => p.PropertyId == methodParameter.PropertyId)?.Value;

            Belonging belonging = CalculateBelongingDegree(
                projectParameterValue,
                methodParameter.FirstParameters,
                methodParameter.SecondParameters);

            calculation!.UpdateItem(property!.Name, belonging);
        }

        calculationRepository.Update(calculation);
        return Result.Success();
    }

    public Belonging CalculateBelongingDegree(
        double? nullableProjectValue,
        ParameterValueGroup? methodFirst,
        ParameterValueGroup? methodSecond)
    {
        if (nullableProjectValue is null)
        {
            return new Belonging(-1);
        }

        double projectValue = (double)nullableProjectValue;

        double degree;


        if (methodFirst is not null && methodSecond is not null)
        {
            if (projectValue <= methodFirst.Min)
            {
                return new Belonging(-1);
            }

            if (projectValue <= methodFirst.Max)
            {
                return new Belonging(CalculateDegreeByFormula(
                    projectValue,
                    methodFirst.Min,
                    methodFirst.Avg,
                    methodFirst.Max, 1));
            }

            if (projectValue < methodSecond.Min)
            {
                return new Belonging(1);
            }

            if (projectValue <= methodSecond.Max)
            {
                return new Belonging(CalculateDegreeByFormula(
                    projectValue,
                    methodSecond.Min,
                    methodSecond.Avg,
                    methodSecond.Max,
                    2));
            }

            return new Belonging(-1);
        }

        if (methodFirst is not null)
        {
            if (projectValue < methodFirst.Min)
            {
                return new Belonging(-1);
            }

            if (projectValue <= methodFirst.Max)
            {
                return new Belonging(CalculateDegreeByFormula(
                    projectValue,
                    methodFirst.Min,
                    methodFirst.Avg,
                    methodFirst.Max,
                    1));
            }

            if (projectValue > methodFirst.Max)
            {
                return new Belonging(1);
            }
        }

        if (methodSecond is not null)
        {
            if (projectValue <= methodSecond.Min)
            {
                return new Belonging(-1);
            }

            if (projectValue <= methodSecond.Max)
            {
                return new Belonging(CalculateDegreeByFormula(
                    projectValue,
                    methodSecond.Min,
                    methodSecond.Avg,
                    methodSecond.Max,
                    2));
            }

            if (projectValue > methodSecond.Max)
            {
                return new Belonging(1);
            }
        }

        throw new InvalidOperationException();
    }

    private static double CalculateDegreeByFormula(double x, double min, double avg, double max, double i)
    {
        double leftOperand = Math.Pow((max - avg) / (avg - min), 2);
        double rightOperand = Math.Pow((x - min) / (max - x), 2);
        double temp = Math.Pow(leftOperand * rightOperand, i * -1);

        return Math.Pow(1 + temp, -1);
    }
}
