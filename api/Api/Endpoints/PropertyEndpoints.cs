using Api.Contracts;
using Api.Contracts.Property;
using Api.Infrastructure;
using Application.Property.Create;
using Application.Property.Delete;
using Application.Property.Get;
using Application.Property.GetById;
using Application.Property.Update;
using Carter;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Results;
using CreatePropertyResponse = Application.Property.Create.CreatePropertyResponse;
using GetPropertiesResponse = Api.Contracts.Property.GetPropertiesResponse;
using GetPropertyByIdResponse = Api.Contracts.Property.GetPropertyByIdResponse;

namespace Api.Endpoints;

public class PropertyEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder? group = app.MapGroup("api/properties").WithTags("properties");

        group.MapPost("", async (
                CreatePropertyRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new CreatePropertyCommand(request.Name, request.Unit);
                Result<CreatePropertyResponse>? result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Created, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces<CreatePropertyResponse>(201)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Create property");

        group.MapDelete("{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new DeletePropertyCommand(id);
                Result? result = await sender.Send(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Delete property");

        group.MapGet("", async (
                [AsParameters] QueryParameters queryParameters,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetPropertiesQuery
                {
                    PageSize = queryParameters.PageSize,
                    PageNumber = queryParameters.PageNumber,
                    SearchTerm = queryParameters.SearchTerm,
                    SortColumn = queryParameters.SortColumn,
                    SortOrder = queryParameters.SortOrder,
                };
                Result<PaginatedList<Application.Property.Get.GetPropertiesResponse>>? result =
                    await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces<GetPropertiesResponse>()
            .Produces(500)
            .WithName("Get properties");

        group.MapGet("{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetPropertyByIdQuery(id);
                Result<Application.Property.GetById.GetPropertyByIdResponse>? result =
                    await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces<GetPropertyByIdResponse>()
            .ProducesProblem(404)
            .WithName("Get property by id");

        group.MapPatch("{id:guid}", async (
                Guid id,
                UpdatePropertyRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdatePropertyCommand(id, request.Name, request.Unit);
                Result? result = await sender.Send(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .Produces(401)
            .Produces(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Update property");
    }
}
