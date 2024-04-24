using Api.Contracts.Project;
using Api.Infrastructure;
using Application.Project.Create;
using Application.Project.GetProjectById;
using Application.Project.GetProjects;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Results;
using CreateProjectParameter = Application.Project.Create.CreateProjectParameter;

namespace Api.Endpoints;

public class ProjectEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/projects").WithTags("projects");

        group.MapPost("", async (CreateProjectRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateProjectCommand
                {
                    Name = request.Name,
                    Country = request.Country,
                    Operator = request.Operator,
                    ProjectType = request.Type,
                    CollectorType = request.CollectorType,
                    MethodIds = request.MethodIds,
                    Parameters = request.Parameters.Select(p => new CreateProjectParameter
                    {
                        Value = p.Value,
                        PropertyId = p.PropertyId
                    }).ToList()
                };

                var result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Created, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces(201)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Create project");

        group.MapGet("", async (
                string? searchTerm,
                string? sortColumn,
                [FromQuery] SortOrder sortOrder,
                int? pageNumber,
                int? pageSize,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetProjectsQuery
                {
                    SortOrder = sortOrder,
                    PageNumber = pageNumber ?? 1,
                    PageSize = pageSize ?? 10,
                    SortColumn = sortColumn,
                    SearchTerm = searchTerm
                };

                var result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces(200)
            .ProducesProblem(500)
            .WithName("Get projects");

        group.MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetProjectByIdQuery { Id = id };
                var result = await sender.Send(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(500);
    }
}