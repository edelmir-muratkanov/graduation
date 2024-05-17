using Domain.Methods;
using Domain.Projects;

namespace Domain.Calculation;

/// <summary>
/// Интерфейс сервиса для создания и обновления расчетов.
/// </summary>
public interface ICalculationService
{
    /// <summary>
    /// Создает новый расчет для указанного проекта и метода.
    /// </summary>
    /// <param name="project">Проект, для которого выполняется расчет.</param>
    /// <param name="method">Метод расчета.</param>
    /// <returns>Результат операции.</returns>
    Task<Result> Create(Project project, Method method);

    /// <summary>
    /// Обновляет существующий расчет для указанного проекта и метода.
    /// </summary>
    /// <param name="project">Проект, для которого выполняется обновление расчета.</param>
    /// <param name="method">Метод расчета.</param>
    /// <returns>Результат операции.</returns>
    Task<Result> Update(Project project, Method method);

    /// <summary>
    /// Вычисляет степень принадлежности для указанного значения геолого-физического параметра проекта и геолого-физических параметров метода.
    /// </summary>
    /// <param name="nullableProjectValue">Значение геолого-физического параметра проекта.</param>
    /// <param name="methodFirst">Первая группа геолого-физического параметров метода.</param>
    /// <param name="methodSecond">Вторая группа геолого-физического параметров метода.</param>
    /// <returns>Степень принадлежности.</returns>
    Belonging CalculateBelongingDegree(
        double? nullableProjectValue,
        ParameterValueGroup? methodFirst,
        ParameterValueGroup? methodSecond);
}
