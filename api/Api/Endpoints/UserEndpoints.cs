using Api.Infrastructure;
using Application.Users.GetUsers;
using Carter;
using MediatR;
using Shared;
using Shared.Results;

namespace Api.Endpoints;

public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/users").WithTags("users").WithOpenApi();

        group.MapGet("/", MapGetList)
            .Produces<PaginatedList<UserResponse>>()
            .ProducesProblem(500)
            .WithSummary("Get users list");
    }

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
