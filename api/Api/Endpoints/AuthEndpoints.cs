using Api.Infrastructure;
using Application.Auth;
using Application.Auth.GetProfile;
using Application.Auth.Login;
using Application.Auth.Refresh;
using Application.Auth.Register;
using Carter;
using MediatR;
using Shared.Results;

namespace Api.Endpoints;

public class AuthEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth").WithTags("auth");

        group.MapGet("profile", async (
                ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetProfileQuery();
                var result = await sender.Send(query, cancellationToken);

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
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);

                if (result.IsFailure) return CustomResults.Problem(result);

                context.Response.Cookies.Append(AuthConstants.AccessTokenKey, result.Value.AccessToken);
                context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken);

                var response = new Contracts.Auth.LoginResponse(
                    result.Value.Id,
                    result.Value.Email,
                    result.Value.Role,
                    result.Value.AccessToken);

                return Results.Ok(response);
            })
            .Produces<Contracts.Auth.LoginResponse>()
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Login");

        group.MapPost("refresh", async (
                ISender sender,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var access = context.Request.Cookies[AuthConstants.AccessTokenKey];
                var refresh = context.Request.Cookies[AuthConstants.RefreshTokenKey];

                var command = new RefreshCommand(access!, refresh!);

                var result = await sender.Send(command, cancellationToken);

                if (result.IsFailure) return Results.Unauthorized();

                var response = new Contracts.Auth.RefreshResponse(result.Value.AccessToken);

                context.Response.Cookies.Append(AuthConstants.AccessTokenKey, result.Value.AccessToken);
                context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken);

                return Results.Ok(response);
            })
            .Produces<Contracts.Auth.RefreshResponse>()
            .Produces(401)
            .ProducesProblem(500)
            .WithName("Refresh");

        group.MapPost("api/auth/register", async (
                RegisterCommand request,
                HttpContext context,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);

                if (result.IsFailure) return CustomResults.Problem(result);

                context.Response.Cookies.Append(
                    AuthConstants.AccessTokenKey,
                    result.Value.AccessToken);

                context.Response.Cookies.Append(
                    AuthConstants.RefreshTokenKey,
                    result.Value.RefreshToken);

                var response = new Contracts.Auth.RegisterResponse(
                    result.Value.Id,
                    result.Value.Email,
                    result.Value.Role,
                    result.Value.AccessToken);

                return Results.Created("api/auth/profile", response);
            })
            .Produces<Contracts.Auth.RegisterResponse>(201)
            .ProducesProblem(400)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Register");
    }
}