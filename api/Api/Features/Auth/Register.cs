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

public static class Register
{
    public record Response(Guid Id, string Email, string Role, string AccessToken, string RefreshToken);

    public sealed record Command(string Email, string Password) : ICommand<Response>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithErrorCode(AuthErrorCodes.Register.MissingEmail)
                .EmailAddress().WithErrorCode(AuthErrorCodes.Register.InvalidEmail);

            RuleFor(c => c.Password)
                .NotEmpty().WithErrorCode(AuthErrorCodes.Register.MissingPassword)
                .MinimumLength(6).WithErrorCode(AuthErrorCodes.Register.ShortPassword);
        }
    }

    internal sealed class Handler(
        ApplicationDbContext context,
        IPasswordManager passwordManager,
        IJwtTokenProvider jwtTokenProvider)
        : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (await context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            {
                return Result.Failure<Response>(UserErrors.EmailNotUnique);
            }

            var password = passwordManager.HashPassword(request.Password);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Password = password,
                Role = Role.User
            };

            var token = jwtTokenProvider.Generate(user);
            var refresh = jwtTokenProvider.GenerateRefreshToken();

            user.Token = refresh;

            user.DomainEvents.Add(new UserRegisteredDomainEvent(user));

            context.Users.Add(user);

            await context.SaveChangesAsync(cancellationToken);

            return new Response(user.Id, user.Email, user.Role.ToString(), token, refresh);
        }
    }
}

public class RegisterEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/register", async (
                RegisterRequest request,
                HttpContext context,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new Register.Command(request.Email, request.Password);
                var result = await sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return CustomResults.Problem(result);
                }

                context.Response.Cookies.Append(AuthConstants.AccessTokenKey, result.Value.AccessToken);
                context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken);

                var response = new RegisterResponse(
                    result.Value.Id,
                    result.Value.Email,
                    result.Value.Role,
                    result.Value.AccessToken);

                return Results.Ok(response);
            })
            .Produces<RegisterResponse>(200)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .ProducesProblem(400)
            .WithName("Register")
            .WithTags("auth");
    }
}