namespace Domain.Calculation;

public static class CalculationErrors
{
    public static readonly Error DuplicateItems = Error.Conflict(
        "Calculation.DuplicateItems", "Результат расчета уже содержит указанный элемент");

    public static readonly Error ItemNotFound = Error.NotFound(
        "Calculation.ItemNotFound", "Элемент расчета не найден");
}
