using Api.Contracts.Auth;
using Api.Domain.Users;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using MediatR;

namespace Api.Features.Auth;

public class RefreshEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup("api/auth")
            .MapPost("refresh", async (ISender sender, HttpContext context, CancellationToken cancellationToken) =>
            {
                var access = context.Request.Cookies[AuthConstants.AccessTokenKey];
                var refresh = context.Request.Cookies[AuthConstants.RefreshTokenKey];

                var command = new Refresh.RefreshCommand(access!, refresh!);

                var result = await sender.Send(command, cancellationToken);

                if (result.IsFailure) return Results.Unauthorized();

                var response = new RefreshResponse(result.Value.AccessToken);

                context.Response.Cookies.Append(AuthConstants.AccessTokenKey, result.Value.AccessToken);
                context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken);

                return Results.Ok(response);
            })
            .Produces<RefreshResponse>()
            .Produces(401)
            .ProducesProblem(500)
            .WithName("Refresh")
            .WithTags("auth");
    }
}

public static class Refresh
{
    public record RefreshResponse(string AccessToken, string RefreshToken);

    public record RefreshCommand(string AccessToken, string RefreshToken) : ICommand<RefreshResponse>;

    internal sealed class Validator : AbstractValidator<RefreshCommand>
    {
        public Validator()
        {
            RuleFor(c => c.RefreshToken)
                .NotEmpty();

            RuleFor(c => c.AccessToken)
                .NotEmpty();
        }
    }

    internal sealed class Handler(
        IJwtTokenProvider jwtTokenProvider,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RefreshCommand, RefreshResponse>
    {
        public async Task<Result<RefreshResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var userId = await jwtTokenProvider.GetUserFromToken(request.AccessToken);

            if (userId is null) return Result.Failure<RefreshResponse>(UserErrors.Unauthorized);

            if (!Guid.TryParse(userId, out var id)) return Result.Failure<RefreshResponse>(UserErrors.Unauthorized);

            var user = await userRepository.GetByIdAsync(id, cancellationToken);

            if (user is null || user.Token != request.RefreshToken)
                return Result.Failure<RefreshResponse>(UserErrors.Unauthorized);

            var access = jwtTokenProvider.Generate(user);
            var refresh = jwtTokenProvider.GenerateRefreshToken();

            user.Token = refresh;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new RefreshResponse(access, refresh);
        }
    }
}