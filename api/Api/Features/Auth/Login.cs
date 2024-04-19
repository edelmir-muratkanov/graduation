﻿using Api.Contracts.Auth;
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
            .ProducesProblem(404)
            .ProducesProblem(400)
            .ProducesProblem(500)
            .WithName("Login")
            .WithTags("auth")
            .WithOpenApi();
    }
}

public static class Login
{
    public record Response(Guid Id, string Email, string Role, string AccessToken, string RefreshToken);

    public sealed record Command(string Email, string Password) : ICommand<Response>;

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
        IJwtTokenProvider jwtTokenProvider) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user is null)
            {
                return Result.Failure<Response>(UserErrors.NotFoundByEmail(request.Email));
            }

            if (!passwordManager.VerifyPassword(user.Password, request.Password))
            {
                return Result.Failure<Response>(UserErrors.InvalidCredentials);
            }

            var token = jwtTokenProvider.Generate(user);
            var refresh = jwtTokenProvider.GenerateRefreshToken();

            user.Token = refresh;

            await context.SaveChangesAsync(cancellationToken);

            return new Response(user.Id, user.Email, user.Role.ToString(), token, refresh);
        }
    }
}