using Microsoft.Extensions.Logging;

namespace Application.Abstractions.Behaviours;

public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Processing request {RequestName}", requestName);

        var result = await next();

        if (result.IsSuccess)
            logger.LogInformation("Completed request {RequestName}", requestName);
        else
            logger.LogError("Completed request {RequestName} with error", requestName);

        return result;
    }
}