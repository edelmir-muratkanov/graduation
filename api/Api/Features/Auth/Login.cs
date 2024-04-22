using Api.Contracts.Auth;
using Api.Domain.Users;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using MediatR;

namespace Api.Features.Auth;

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/login", async (
                LoginRequest request,
                ISender sender,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var command = new Login.LoginCommand(request.Email, request.Password);
                var result = await sender.Send(command, cancellationToken);

                if (result.IsFailure) return CustomResults.Problem(result);

                context.Response.Cookies.Append(AuthConstants.AccessTokenKey, result.Value.AccessToken);
                context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken);

                var response = new LoginResponse(
                    result.Value.Id,
                    result.Value.Email,
                    result.Value.Role,
                    result.Value.AccessToken);

                return Results.Ok(response);
            })
            .Produces<LoginResponse>()
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Login")
            .WithTags("auth")
            .WithOpenApi();
    }
}

public static class Login
{
    public record LoginResponse(Guid Id, string Email, string Role, string AccessToken, string RefreshToken);

    public sealed record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;

    internal sealed class Validator : AbstractValidator<LoginCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithErrorCode(AuthErrorCodes.Login.MissingEmail)
                .EmailAddress().WithErrorCode(AuthErrorCodes.Login.InvalidEmail);

            RuleFor(c => c.Password)
                .NotEmpty().WithErrorCode(AuthErrorCodes.Login.MissingPassword)
                .MinimumLength(6).WithErrorCode(AuthErrorCodes.Login.ShortPassword);
        }
    }

    internal sealed class Handler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordManager passwordManager,
        IJwtTokenProvider jwtTokenProvider) : ICommandHandler<LoginCommand, LoginResponse>
    {
        public async Task<Result<LoginResponse>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null) return Result.Failure<LoginResponse>(UserErrors.NotFoundByEmail(request.Email));

            if (!passwordManager.VerifyPassword(user.Password, request.Password))
                return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);

            var token = jwtTokenProvider.Generate(user);
            var refresh = jwtTokenProvider.GenerateRefreshToken();

            user.Token = refresh;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoginResponse(user.Id, user.Email, user.Role.ToString(), token, refresh);
        }
    }
}