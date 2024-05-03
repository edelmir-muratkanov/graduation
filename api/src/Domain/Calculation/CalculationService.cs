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
            if (projectValue <= methodFirst.Max)
            {
                degree = CalculateDegreeByFormula(projectValue, methodFirst.Min, methodFirst.Avg, methodFirst.Max, 1);
                return new Belonging(degree);
            }

            if (projectValue < methodSecond.Min)
            {
                return new Belonging(1);
            }

            if (projectValue > methodSecond.Max)
            {
                return new Belonging(-1);
            }

            degree = CalculateDegreeByFormula(projectValue, methodSecond.Min, methodSecond.Avg, methodSecond.Max, 2);
            return new Belonging(degree);
        }

        if (methodFirst is not null)
        {
            degree = CalculateDegreeByFormula(
                projectValue,
                methodFirst.Min,
                methodFirst.Avg,
                methodFirst.Max,
                1);
            return new Belonging(degree);
        }

        if (methodSecond is not null)
        {
            degree = CalculateDegreeByFormula(projectValue, methodSecond.Min, methodSecond.Avg, methodSecond.Max, 2);

            return new Belonging(degree);
        }

        throw new InvalidOperationException();
    }

    private static double CalculateDegreeByFormula(double x, double min, double avg, double max, double i)
    {
        if (x <= min)
        {
            return -1;
        }

        if (x > max)
        {
            return 1;
        }

        double leftOperand = Math.Pow((max - avg) / (avg - min), 2);
        double rightOperand = Math.Pow((x - min) / (max - x), 2);

        return Math.Pow(1 + leftOperand * rightOperand * -1, i) * -1;
    }
}
