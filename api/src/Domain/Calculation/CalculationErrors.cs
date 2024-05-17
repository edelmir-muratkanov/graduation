namespace Domain.Calculation;

/// <summary>
/// Содержит стандартные ошибки, связанные с операциями над результатами расчетов.
/// </summary>
public static class CalculationErrors
{
    /// <summary>
    /// Ошибка, указывающая на то, что результат расчета не был найден.
    /// </summary>
    /// 
    public static readonly Error NotFound = Error.NotFound("Calculation.NotFound", "Результат расчета не найден");

    /// <summary>
    /// Ошибка, указывающая на конфликт из-за дублирования элементов в результате расчета.
    /// </summary>
    public static readonly Error DuplicateItems = Error.Conflict(
        "Calculation.DuplicateItems", "Результат расчета уже содержит указанный элемент");

    /// <summary>
    /// Ошибка, указывающая на то, что элемент расчета не был найден.
    /// </summary>
    public static readonly Error ItemNotFound = Error.NotFound(
        "Calculation.ItemNotFound", "Элемент расчета не найден");

    /// <summary>
    /// Ошибка, указывающая на конфликт из-за существования уже существующего результата расчета.
    /// </summary>
    public static readonly Error AlreadyExists = Error.Conflict(
        "Calculation.AlreadyExists", "Результат расчета существует");
}
