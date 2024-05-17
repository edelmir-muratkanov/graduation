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
using AuthContracts = Api.Contracts.Auth;

namespace Api.Endpoints;

/// <summary>
/// Класс, представляющий конечные точки аутентификации и авторизации.
/// </summary>
public class AuthEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Определение конечных точек для различных операций аутентификации
        // и регистрации пользователей.
        
        RouteGroupBuilder? group = app
            .MapGroup("api/auth")
            .WithTags("auth")
            .WithOpenApi();


        group.MapGet("profile", MapGetProfile)
            .RequireAuthorization()
            .Produces<GetProfileResponse>()
            .Produces(401)
            .WithSummary("Get user profile")
            .WithName("Get profile");

        group.MapPost("login", MapPostLogin)
            .Produces<AuthContracts.LoginResponse>()
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Login user")
            .WithName("Login");

        group.MapPost("refresh", MapPostRefresh)
            .Produces<AuthContracts.RefreshResponse>()
            .Produces(401)
            .ProducesProblem(500)
            .WithSummary("Refresh tokens")
            .WithName("Refresh");

        group.MapPost("register", MapPostRegister)
            .Produces<AuthContracts.RegisterResponse>(201)
            .ProducesProblem(400)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithSummary("Register user")
            .WithName("Register");

        group.MapPost("logout", MapPostLogout)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(500)
            .WithSummary("Logout user")
            .WithName("Logout");
    }

    // Обработчики для каждой конечной точки
    
    private static Task<IResult> MapPostLogout(HttpContext context, CancellationToken cancellationToken)
    {
        context.Response.Cookies.Delete(AuthConstants.RefreshTokenKey);
        return Task.FromResult(Results.Ok());
    }

    private static async Task<IResult> MapPostRegister(
        RegisterCommand request,
        HttpContext context,
        ISender sender,
        IOptions<JwtOptions> jwtOptions,
        CancellationToken cancellationToken)
    {
        Result<RegisterResponse> result = await sender.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            return CustomResults.Problem(result);
        }

        context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(jwtOptions.Value.RefreshExpiryInDays)
        });

        var response = new AuthContracts.RegisterResponse(
            result.Value.Id,
            result.Value.Email,
            result.Value.Role,
            result.Value.AccessToken);

        return Results.Created("api/auth/profile", response);
    }

    private static async Task<IResult> MapPostRefresh(
        ISender sender,
        HttpContext context,
        IOptions<JwtOptions> jwtOptions,
        CancellationToken cancellationToken)
    {
        if (!context.Request.Headers.ContainsKey("Authorization") ||
            context.Request.Headers.Authorization.Count == 0 ||
            !context.Request.Headers.Authorization[0]!.StartsWith("Bearer "))
        {
            return Results.Unauthorized();
        }

        string access = context.Request.Headers.Authorization[0]!["Bearer ".Length..];

        context.Request.Cookies.TryGetValue(AuthConstants.RefreshTokenKey, out string? refresh);

        if (string.IsNullOrWhiteSpace(refresh))
        {
            return Results.Unauthorized();
        }

        var command = new RefreshCommand(access, refresh);

        Result<RefreshResponse> result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Results.Unauthorized();
        }

        var response = new AuthContracts.RefreshResponse(result.Value.AccessToken);

        context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(jwtOptions.Value.RefreshExpiryInDays)
        });

        return Results.Ok(response);
    }

    private static async Task<IResult> MapPostLogin(
        LoginCommand request,
        ISender sender,
        HttpContext context,
        IOptions<JwtOptions> jwtOptions,
        CancellationToken cancellationToken)
    {
        Result<LoginResponse> result = await sender.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            return CustomResults.Problem(result);
        }

        context.Response.Cookies.Append(AuthConstants.RefreshTokenKey, result.Value.RefreshToken, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(jwtOptions.Value.RefreshExpiryInDays)
        });

        var response = new AuthContracts.LoginResponse(
            result.Value.Id,
            result.Value.Email,
            result.Value.Role,
            result.Value.AccessToken);

        return Results.Ok(response);
    }


    private static async Task<IResult> MapGetProfile(ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetProfileQuery();
        Result<GetProfileResponse>? result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
