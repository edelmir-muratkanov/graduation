using Microsoft.Extensions.Logging;

namespace Application.Abstractions.Behaviours;

/// <summary>
/// Поведение для обработки необработанных исключений.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип ответа.</typeparam>
/// <param name="logger">Логгер.</param>
public class UnhandledExceptionBehaviour<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Обрабатывает запрос и перехватывает необработанные исключения.
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
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            string? requestName = typeof(TRequest).Name;
            logger.LogError(
                e,
                "Request: Unhandled Exception for Request {Name} {@Request}",
                requestName,
                request);
            throw;
        }
    }
}
