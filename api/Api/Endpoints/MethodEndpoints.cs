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
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Results;

namespace Api.Endpoints;

public class MethodEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/methods").WithTags("methods");

        group.MapPost("", async (
                CreateMethodCommand request,
                ISender sender) =>
            {
                Result result = await sender.Send(request);
                return result.Match(Results.Created, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(201)
            .ProducesProblem(400)
            .Produces(401)
            .Produces(403)
            .ProducesProblem(409)
            .Produces(500)
            .WithName("Create method");

        group.MapPost("{id:guid}/parameters", async (
                Guid id,
                List<AddMethodParametersRequest> parametersRequests,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                List<MethodParameter> parameters = parametersRequests.Adapt<List<MethodParameter>>();

                var command = new AddMethodParametersCommand(id, parameters);

                Result result = await sender.Send(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Add method parameters");

        group.MapDelete("{methodId:guid}/parameters/{parameterId:guid}", async (
                Guid methodId,
                Guid parameterId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new RemoveMethodParameterCommand
                {
                    MethodId = methodId,
                    ParameterId = parameterId
                };

                Result result = await sender.Send(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Remove method parameter");


        group.MapGet("", async (
                string? searchTerm,
                string? sortColumn,
                [FromQuery] SortOrder sortOrder,
                int? pageNumber,
                int? pageSize,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetMethodsQuery
                {
                    SortOrder = sortOrder,
                    PageNumber = pageNumber ?? 1,
                    PageSize = pageSize ?? 10,
                    SearchTerm = searchTerm,
                    SortColumn = sortColumn
                };

                Result<PaginatedList<GetMethodsResponse>> result = await sender.Send(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces(200)
            .Produces(500)
            .WithName("Get methods");

        group.MapGet("{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetMethodByIdQuery { Id = id };
                Result<GetMethodByIdResponse> result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces<GetMethodByIdResponse>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Get method");

        group.MapPatch("{id:guid}", async (
                Guid id,
                UpdateMethodCommand request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                Result result = await sender.Send(request, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .Produces(401)
            .Produces(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Update method");

        group.MapDelete("{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteMethodCommand { Id = id };
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500);
    }
}
