using Api.Infrastructure;
using Application.Project.GetProjects;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Results;

namespace Api.Endpoints;

public class ProjectEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/projects").WithTags("projects");

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
    }
}