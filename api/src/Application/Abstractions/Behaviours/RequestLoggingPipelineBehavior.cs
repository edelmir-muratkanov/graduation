using Microsoft.Extensions.Logging;

namespace Application.Abstractions.Behaviours;

/// <summary>
/// Поведение для логирования запросов и результатов.
/// </summary>
/// <param name="logger">Логгер.</param>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип ответа.</typeparam>
public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    /// <summary>
    /// Обрабатывает запрос и выполняет логирование.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="next">Делегат для вызова следующего обработчика в цепочке.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат обработки запроса.</returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string? requestName = typeof(TRequest).Name;

        logger.LogInformation("Processing request {RequestName}", requestName);

        TResponse? result = await next();

        if (result.IsSuccess)
        {
            logger.LogInformation("Completed request {RequestName}", requestName);
        }
        else
        {
            logger.LogError("Completed request {RequestName} with error", requestName);
        }

        return result;
    }
}
