using Api.Infrastructure;
using Application.Users.GetUsers;
using Carter;
using MediatR;
using Shared;
using Shared.Results;

namespace Api.Endpoints;

/// <summary>
/// Класс, представляющий конечные точки пользователей.
/// </summary>
public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Определение конечных точек для различных операций с пользователями.

        RouteGroupBuilder group = app.MapGroup("api/users").WithTags("users").WithOpenApi();

        group.MapGet("/", MapGetList)
            .Produces<PaginatedList<UserResponse>>()
            .ProducesProblem(500)
            .WithSummary("Get users list");
    }

    // Методы-обработчики для каждой конечной точки

    private static async Task<IResult> MapGetList(
        string? searchTerm,
        SortOrder? sortOrder,
        int? pageNumber,
        int? pageSize,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery
        {
            SortOrder = sortOrder,
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm
        };
        Result<PaginatedList<UserResponse>> result = await sender.Send(query, cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
