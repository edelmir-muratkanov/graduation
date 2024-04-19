using Api.Contracts.Auth;
using Api.Domain.Users;
using Api.Infrastructure.Database;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                var command = new Login.Command(request.Email, request.Password);
                var result = await sender.Send(command, cancellationToken);

                if (result.IsSuccess)
                {
                    context.Response.Cookies.Append("x-token", result.Value.Token);
                }

                return result.Match(Results.Ok, CustomResults.Problem);
            }).Produces<LoginResponse>()
            .ProducesProblem(404)
            .ProducesProblem(400)
            .ProducesProblem(500)
            .WithTags("Login")
            .WithTags("auth");
    }
}

public static class Login
{
    public sealed record Command(string Email, string Password) : ICommand<LoginResponse>;

    internal sealed class Validator : AbstractValidator<Command>
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
        ApplicationDbContext context,
        IPasswordManager passwordManager,
        IJwtTokenProvider jwtTokenProvider) : ICommandHandler<Command, LoginResponse>
    {
        public async Task<Result<LoginResponse>> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user is null)
            {
                return Result.Failure<LoginResponse>(UserErrors.NotFoundByEmail(request.Email));
            }

            if (!passwordManager.VerifyPassword(user.Password, request.Password))
            {
                return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
            }

            var token = jwtTokenProvider.Generate(user);

            return new LoginResponse(user.Id, user.Email, user.Role.ToString(), token);
        }
    }
}