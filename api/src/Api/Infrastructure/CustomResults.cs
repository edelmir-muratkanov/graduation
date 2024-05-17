using Shared.Results;

namespace Api.Infrastructure;

/// <summary>
/// Класс для создания пользовательских результатов HTTP-ответов.
/// </summary>
public static class CustomResults
{
    /// <summary>
    /// Создает HTTP-ответ с проблемой на основе объекта Result.
    /// </summary>
    /// <param name="result">Объект Result, содержащий информацию об ошибке.</param>
    /// <returns>Объект IResult, представляющий проблему.</returns>
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.Type),
            statusCode: GetStatusCode(result.Error.Type),
            extensions: GetErrors(result));

        // Получение заголовка ошибки на основе типа ошибки.
        static string GetTitle(Error error)
        {
            return error.Type switch
            {
                ErrorType.Validation => error.Code,
                ErrorType.Problem => error.Code,
                ErrorType.NotFound => error.Code,
                ErrorType.Conflict => error.Code,
                ErrorType.Forbidden => error.Code,
                _ => "Server failure"
            };
        }

        // Получение подробного описания ошибки.
        static string GetDetail(Error error)
        {
            return error.Type switch
            {
                ErrorType.Validation => error.Description,
                ErrorType.Problem => error.Description,
                ErrorType.NotFound => error.Description,
                ErrorType.Conflict => error.Description,
                ErrorType.Forbidden => error.Description,
                _ => "An unexpected error occurred"
            };
        }

        // Получение ссылки на описание типа ошибки.
        static string GetType(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                ErrorType.Problem => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                ErrorType.Forbidden => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
        }

        // Получение HTTP-кода состояния на основе типа ошибки.
        static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        // Получение ошибок в случае ошибки валидации.
        static Dictionary<string, object?>? GetErrors(Result result)
        {
            if (result.Error is not ValidationError validationError)
            {
                return null;
            }

            return new Dictionary<string, object?>
            {
                { "errors", validationError.Errors }
            };
        }
    }
}
