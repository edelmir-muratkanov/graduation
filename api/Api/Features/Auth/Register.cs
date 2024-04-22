﻿using Api.Contracts.Auth;
using Api.Domain.Users;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using MediatR;

namespace Api.Features.Auth;

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
                var command = new Register.RegisterCommand(request.Email, request.Password);
                var result = await sender.Send(command, cancellationToken);

                if (result.IsFailure) return CustomResults.Problem(result);

                context.Response.Cookies.Append(AuthConstants.AccessTokenKey, result.Value.AccessToken);
                context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken);

                var response = new RegisterResponse(
                    result.Value.Id,
                    result.Value.Email,
                    result.Value.Role,
                    result.Value.AccessToken);

                return Results.Created<RegisterResponse>("api/auth/profile", response);
            })
            .Produces<RegisterResponse>(201)
            .ProducesProblem(400)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Register")
            .WithTags("auth");
    }
}

public static class Register
{
    public record RegisterResponse(Guid Id, string Email, string Role, string AccessToken, string RefreshToken);

    public sealed record RegisterCommand(string Email, string Password) : ICommand<RegisterResponse>;

    public class Validator : AbstractValidator<RegisterCommand>
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
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordManager passwordManager,
        IJwtTokenProvider jwtTokenProvider)
        : ICommandHandler<RegisterCommand, RegisterResponse>
    {
        public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (!await userRepository.IsEmailUniqueAsync(request.Email))
                return Result.Failure<RegisterResponse>(UserErrors.EmailNotUnique);

            var password = passwordManager.HashPassword(request.Password);
            var userResult = User.Create(request.Email, password);

            if (userResult.IsFailure) return Result.Failure<RegisterResponse>(userResult.Error);


            var token = jwtTokenProvider.Generate(userResult.Value);
            var refresh = jwtTokenProvider.GenerateRefreshToken();

            userResult.Value.UpdateToken(refresh);

            userRepository.Insert(userResult.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new RegisterResponse(
                userResult.Value.Id,
                userResult.Value.Email,
                userResult.Value.Role.ToString(),
                token,
                refresh);
        }
    }
}