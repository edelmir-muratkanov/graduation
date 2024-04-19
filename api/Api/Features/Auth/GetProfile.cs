using Api.Contracts.Auth;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using MediatR;

namespace Api.Features.Auth;

public class GetProfileEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGroup("api/auth").MapGet("profile", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetProfile.Query();
            var result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).RequireAuthorization().WithTags("auth");
    }
}

public static class GetProfile
{
    public sealed record Query : IQuery<GetProfileResponse>;

    internal sealed class Handler(ICurrentUserService currentUserService)
        : IQueryHandler<Query, GetProfileResponse>
    {
        public Task<Result<GetProfileResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (currentUserService.Id != null && currentUserService is { Role: not null, Email: not null })
            {
                return Task.FromResult<Result<GetProfileResponse>>(new GetProfileResponse(currentUserService.Id,
                    currentUserService.Email, currentUserService.Role));
            }

            return null!;
        }
    }
}