namespace Api.Shared.Models;

public sealed record ValidationError(Error[] Errors)
    : Error("General.Validation", "Ошибка валидации", ErrorType.Validation)
{
    public static ValidationError FromResults(IEnumerable<Result> results)
    {
        return new ValidationError(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
    }
}