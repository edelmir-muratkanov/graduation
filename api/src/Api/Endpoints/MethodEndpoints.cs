using Api.Contracts;
using Api.Contracts.Method;
using Api.Infrastructure;
using Application.Method.AddParameters;
using Application.Method.Create;
using Application.Method.Delete;
using Application.Method.GetMethodById;
using Application.Method.GetMethods;
using Application.Method.RemoveParameter;
using Application.Method.Update;
using Carter;
using Domain.Users;
using Mapster;
using MediatR;
using Shared;
using Shared.Results;

namespace Api.Endpoints;

/// <summary>
/// Класс, представляющий конечные точки методов.
/// </summary>
public class MethodEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Определение конечных точек для различных операций с методами.

        RouteGroupBuilder group = app
            .MapGroup("api/methods")
            .WithTags("methods")
            .WithOpenApi();

        group.MapPost("", MapPostCreateMethod)
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(201)
            .ProducesProblem(400)
            .Produces(401)
            .Produces(403)
            .ProducesProblem(409)
            .Produces(500)
            .WithSummary("Create method")
            .WithName("Create method");

        group.MapGet("", MapGetMethodsList)
            .Produces<PaginatedList<GetMethodsResponse>>(200)
            .Produces(500)
            .WithSummary("Get methods paginated list")
            .WithName("Get methods");

        group.MapGet("{id:guid}", MapGetMethodById)
            .Produces<GetMethodByIdResponse>()
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Get method by id")
            .WithName("Get method");

        group.MapPatch("{id:guid}", MapPatchUpdateMethodBaseInformation)
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .Produces(401)
            .Produces(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Update method base information")
            .WithName("Update method");

        group.MapDelete("{id:guid}", MapDeleteMethod)
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Delete method")
            .WithName("Delete method");


        group.MapPost("{id:guid}/parameters", MapPostAddMethodParameters)
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithSummary("Add method parameters")
            .WithName("Add method parameters");

        group.MapDelete("{methodId:guid}/parameters/{parameterId:guid}", MapDeleteMethodParameter)
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithSummary("Remove method parameter")
            .WithName("Remove method parameter");
    }

    // Методы-обработчики для каждой конечной точки

    private static async Task<IResult> MapDeleteMethodParameter(
        Guid methodId,
        Guid parameterId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new RemoveMethodParameterCommand
        {
            MethodId = methodId,
            ParameterId = parameterId
        };

        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }

    private static async Task<IResult> MapPostAddMethodParameters(
        Guid id,
        List<AddMethodParametersRequest> parametersRequests,
        ISender sender,
        CancellationToken cancellationToken)
    {
        List<MethodParameter> parameters = parametersRequests.Adapt<List<MethodParameter>>();

        var command = new AddMethodParametersCommand(id, parameters);

        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }

    private static async Task<IResult> MapDeleteMethod(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        var command = new DeleteMethodCommand
        {
            Id = id
        };
        Result result = await sender.Send(command, cancellationToken);
        return result.Match(Results.NoContent, CustomResults.Problem);
    }

    private static async Task<IResult> MapPatchUpdateMethodBaseInformation(
        Guid id,
        UpdateMethodBaseInformationRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMethodCommand(id, request.Name, request.CollectorTypes);
        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }

    private static async Task<IResult> MapGetMethodById(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetMethodByIdQuery
        {
            Id = id
        };
        Result<GetMethodByIdResponse> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }

    private static async Task<IResult> MapGetMethodsList(
        [AsParameters] QueryParameters queryParameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetMethodsQuery
        {
            SortOrder = queryParameters.SortOrder,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            SearchTerm = queryParameters.SearchTerm,
            SortColumn = queryParameters.SortColumn
        };

        Result<PaginatedList<GetMethodsResponse>> result = await sender.Send(query, cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }

    private static async Task<IResult> MapPostCreateMethod(CreateMethodCommand request, ISender sender)
    {
        Result result = await sender.Send(request);
        return result.Match(Results.Created, CustomResults.Problem);
    }
}
