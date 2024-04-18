using Api.Shared.Interfaces;
using MediatR.Pipeline;

namespace Api.Shared.Behaviours;

public class LoggingBehaviour<TRequest>(
    ILogger<TRequest> logger,
    ICurrentUserService currentUserService) : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = currentUserService.UserId ?? string.Empty;

        return Task.Run(
            () => logger.LogInformation("Request: {Name} {@UserId} {@Request}",
                requestName,
                userId,
                requestName),
            cancellationToken);
    }
}