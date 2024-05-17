using Domain.Methods;
using Domain.Projects;
using Domain.Properties;

namespace Domain.Calculation;

/// <summary>
/// Сервис для создания и обновления расчетов.
/// </summary>
/// <param name="propertyRepository">Репозиторий свойств.</param>
/// <param name="calculationRepository">Репозиторий расчетов.</param>
public sealed class CalculationService(
    IPropertyRepository propertyRepository,
    ICalculationRepository calculationRepository)
    : ICalculationService
{
    /// <summary>
    /// Создает новый расчет для указанного проекта и метода.
    /// </summary>
    /// <param name="project">Проект, для которого выполняется расчет.</param>
    /// <param name="method">Метод расчета.</param>
    /// <returns>Результат операции.</returns>
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

    /// <summary>
    /// Обновляет существующий расчет для указанного проекта и метода.
    /// </summary>
    /// <param name="project">Проект, для которого выполняется обновление расчета.</param>
    /// <param name="method">Метод расчета.</param>
    /// <returns>Результат операции.</returns>
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

    /// <summary>
    /// Вычисляет степень принадлежности для указанного значения проектного параметра и параметров метода.
    /// </summary>
    /// <param name="nullableProjectValue">Значение проектного параметра.</param>
    /// <param name="methodFirst">Первая группа параметров метода.</param>
    /// <param name="methodSecond">Вторая группа параметров метода.</param>
    /// <returns>Степень принадлежности.</returns>
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

            if (projectValue > methodSecond.Max)
            {
                return new Belonging(-1);
            }
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Вычисляет степень принадлежности по формуле для заданных параметров.
    /// </summary>
    /// <param name="x">Значение геолого-физического параметра проекта.</param>
    /// <param name="min">Минимальное значение геолого-физического параметра метода.</param>
    /// <param name="avg">Среднее значение геолого-физического параметра метода.</param>
    /// <param name="max">Максимальное значение геолого-физического параметра метода.</param>
    /// <param name="i">Коэффициент, представляющее ветвь функции принадлежности</param>
    /// <returns>Степень принадлежности.</returns>
    private static double CalculateDegreeByFormula(double x, double min, double avg, double max, double i)
    {
        double leftOperand = Math.Pow((max - avg) / (avg - min), 2);
        double rightOperand = Math.Pow((x - min) / (max - x), 2);
        double temp = Math.Pow(leftOperand * rightOperand, i * -1);

        return Math.Pow(1 + temp, -1);
    }
}
