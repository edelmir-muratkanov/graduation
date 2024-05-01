using Domain.Methods;

namespace Domain.Calculation;

public sealed class CalculationService : ICalculationService
{
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
