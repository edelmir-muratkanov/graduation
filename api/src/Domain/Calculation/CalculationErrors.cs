namespace Domain.Calculation;

public static class CalculationErrors
{
    public static readonly Error NotFound = Error.NotFound("Calculation.NotFound", "Результат расчета не найден");
    public static readonly Error DuplicateItems = Error.Conflict(
        "Calculation.DuplicateItems", "Результат расчета уже содержит указанный элемент");

    public static readonly Error ItemNotFound = Error.NotFound(
        "Calculation.ItemNotFound", "Элемент расчета не найден");

    public static readonly Error AlreadyExists = Error.Conflict(
        "Calculation.AlreadyExists", "Результат расчета существует");
}
