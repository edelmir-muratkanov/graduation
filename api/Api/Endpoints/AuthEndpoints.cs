using Api.Infrastructure;
using Application.Auth.GetProfile;
using Application.Auth.Login;
using Application.Auth.Refresh;
using Application.Auth.Register;
using Carter;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Results;
using LoginResponse = Api.Contracts.Auth.LoginResponse;
using RefreshResponse = Api.Contracts.Auth.RefreshResponse;
using RegisterResponse = Api.Contracts.Auth.RegisterResponse;

namespace Api.Endpoints;

public class AuthEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder? group = app.MapGroup("api/auth").WithTags("auth");

        group.MapGet("profile", async (
                ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetProfileQuery();
                Result<GetProfileResponse>? result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces<GetProfileResponse>()
            .Produces(401)
            .WithName("Get profile");

        group.MapPost("login", async (
                LoginCommand request,
                ISender sender,
                HttpContext context,
                IOptions<JwtOptions> jwtOptions,
                CancellationToken cancellationToken) =>
            {
                Result<Application.Auth.Login.LoginResponse>? result = await sender.Send(request, cancellationToken);

                if (result.IsFailure)
                {
                    return CustomResults.Problem(result);
                }
                context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken,
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(jwtOptions.Value.RefreshExpiryInDays)
                    });

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
            .WithName("Login");

        group.MapPost("refresh", async (
                ISender sender,
                HttpContext context,
                IOptions<JwtOptions> jwtOptions,
                CancellationToken cancellationToken) =>
            {
                string? access = context.Request.Headers.Authorization[0]?.Split(" ")[1];
                string? refresh = context.Request.Cookies[AuthConstants.RefreshTokenKey];

                var command = new RefreshCommand(access!, refresh!);

                Result<Application.Auth.Refresh.RefreshResponse>? result =
                    await sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return Results.Unauthorized();
                }

                var response = new RefreshResponse(result.Value.AccessToken);
                
                context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken,
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(jwtOptions.Value.RefreshExpiryInDays)
                    });

                return Results.Ok(response);
            })
            .Produces<RefreshResponse>()
            .Produces(401)
            .ProducesProblem(500)
            .WithName("Refresh");

        group.MapPost("api/auth/register", async (
                RegisterCommand request,
                HttpContext context,
                ISender sender,
                IOptions<JwtOptions> jwtOptions,
                CancellationToken cancellationToken) =>
            {
                Result<Application.Auth.Register.RegisterResponse>? result =
                    await sender.Send(request, cancellationToken);

                if (result.IsFailure)
                {
                    return CustomResults.Problem(result);
                }
                
                context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken,
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(jwtOptions.Value.RefreshExpiryInDays)
                    });

                var response = new RegisterResponse(
                    result.Value.Id,
                    result.Value.Email,
                    result.Value.Role,
                    result.Value.AccessToken);

                return Results.Created("api/auth/profile", response);
            })
            .Produces<RegisterResponse>(201)
            .ProducesProblem(400)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Register");
    }
}
