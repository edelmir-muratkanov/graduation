using Api.Contracts.Project;
using Api.Infrastructure;
using Application.Project.AddMembers;
using Application.Project.AddMethods;
using Application.Project.AddParameters;
using Application.Project.Create;
using Application.Project.GetProjectById;
using Application.Project.GetProjects;
using Application.Project.Remove;
using Application.Project.RemoveMember;
using Application.Project.RemoveMethod;
using Application.Project.RemoveParameter;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Results;

namespace Api.Endpoints;

public class ProjectEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/projects").WithTags("projects");

        group.MapPost("", async (CreateProjectRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var createProjectCommand = new CreateProjectCommand
                {
                    Name = request.Name,
                    Country = request.Country,
                    Operator = request.Operator,
                    ProjectType = request.Type,
                    CollectorType = request.CollectorType,
                };

                Result<Guid> createResult = await sender.Send(createProjectCommand, cancellationToken);

                if (createResult.IsFailure)
                {
                    return CustomResults.Problem(createResult);
                }

                var addMethodsCommand = new AddProjectMethodsCommand
                {
                    ProjectId = createResult.Value,
                    MethodIds = request.MethodIds
                };

                Result addMethodsResult = await sender.Send(addMethodsCommand, cancellationToken);

                if (addMethodsResult.IsFailure)
                {
                    return CustomResults.Problem(addMethodsResult);
                }

                var addParametersCommand = new AddProjectParametersCommand
                {
                    ProjectId = createResult.Value,
                    Parameters = request.Parameters.Select(p => new AddProjectParameter
                    {
                        PropertyId = p.PropertyId,
                        Value = p.Value
                    }).ToList()
                };

                Result addParametersResult = await sender.Send(addParametersCommand, cancellationToken);

                return addParametersResult.Match(Results.Created, CustomResults.Problem);
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
                    SortColumn = sortColumn ?? string.Empty,
                    SearchTerm = searchTerm ?? string.Empty
                };

                Result<PaginatedList<GetProjectsResponse>> result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces(200)
            .ProducesProblem(500)
            .WithName("Get projects");

        group.MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetProjectByIdQuery { Id = id };
                Result<GetProjectByIdResponse> result = await sender.Send(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithDescription("Get project");

        group.MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new RemoveProjectCommand { ProjectId = id };
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Delete project");

        group.MapPost("{id:guid}/parameters", async (
                Guid id,
                List<AddProjectParametersRequest> parametersRequests,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                List<AddProjectParameter> parameters = parametersRequests.Adapt<List<AddProjectParameter>>();
                var command = new AddProjectParametersCommand
                {
                    ProjectId = id,
                    Parameters = parameters
                };
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Created, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces(201)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Add project parameters");

        group.MapDelete("{projectId:guid}/parameters/{parameterId:guid}", async (
                Guid projectId,
                Guid parameterId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new RemoveProjectParameterCommand
                {
                    ProjectId = projectId,
                    ParameterId = parameterId
                };
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Remove project parameter");

        group.MapPost("{projectId:guid}/methods", async (
                Guid projectId,
                List<Guid> methodIds,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new AddProjectMethodsCommand
                {
                    ProjectId = projectId,
                    MethodIds = methodIds
                };
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Created, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces(201)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Add project methods");

        group.MapDelete("{projectId:guid}/methods/{methodId:guid}", async (
                Guid projectId,
                Guid methodId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new RemoveProjectMethodCommand
                {
                    ProjectId = projectId,
                    MethodId = methodId
                };
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Remove project method");

        group.MapPost("{projectId:guid}/members", async (
                Guid projectId,
                List<Guid> memberIds,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new AddProjectMembersCommand
                {
                    MemberIds = memberIds,
                    ProjectId = projectId
                };
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Created, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces(201)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Add project members");

        group.MapDelete("{projectId:guid}/members/{memberId:guid}", async (
                Guid projectId,
                Guid memberId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new RemoveProjectMemberCommand
                {
                    MemberId = memberId,
                    ProjectId = projectId
                };
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(500)
            .WithName("Remove project member");
    }
}
