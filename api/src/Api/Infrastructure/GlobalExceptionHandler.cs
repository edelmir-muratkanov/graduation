using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure;

/// <summary>
/// Обработчик исключений для обработки необработанных исключений в приложении.
/// </summary>
internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Пытается обработать исключение.
    /// </summary>
    /// <param name="httpContext">Контекст HTTP-запроса.</param>
    /// <param name="exception">Исключение, которое необходимо обработать.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>True, если исключение успешно обработано, в противном случае - false.</returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Логируем необработанное исключение.
        logger.LogError(exception, "Unhandled exception occured");

        // Если исключение - это некорректный HTTP-запрос (400 Bad Request),
        // обрабатываем его отдельно.
        if (exception is BadHttpRequestException)
        {
            return await HandleBadRequest(httpContext, cancellationToken);
        }

        // Создаем объект ProblemDetails для обработки остальных исключений.
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "Server failure"
        };

        // Устанавливаем код состояния и отправляем объект ProblemDetails как ответ.
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    /// <summary>
    /// Обрабатывает некорректный HTTP-запрос (400 Bad Request).
    /// </summary>
    /// <param name="httpContext">Контекст HTTP-запроса.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>True, если исключение успешно обработано, в противном случае - false.</returns>
    private async ValueTask<bool> HandleBadRequest(
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        // Создаем объект ProblemDetails для некорректного HTTP-запроса.
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Detail = "Ошибка валидации. Проверьте правильны ли данные в запросе"
        };

        // Устанавливаем код состояния и отправляем объект ProblemDetails как ответ.
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
