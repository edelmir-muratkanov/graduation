namespace Shared.Results;

/// <summary>
/// Представляет запись ошибки валидации с массивом ошибок.
/// </summary>
/// <param name="Errors">Спискок ошибок типа <see cref="Error"/>.</param>
public sealed record ValidationError(Error[] Errors)
    : Error("General.Validation", "Ошибка валидации", ErrorType.Validation)
{
    /// <summary>
    /// Создает новый экземпляр класса <see cref="ValidationError"/> на основе коллекции результатов операций.
    /// </summary>
    /// <param name="results">Коллекция результатов операций.</param>
    /// <returns>Новый экземпляр класса <see cref="ValidationError"/> с ошибками из неуспешных результатов операций.</returns>
    public static ValidationError FromResults(IEnumerable<Result> results)
    {
        return new ValidationError(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
    }
}
