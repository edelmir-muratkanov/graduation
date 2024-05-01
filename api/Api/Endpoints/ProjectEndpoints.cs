using Api.Contracts;
using Api.Contracts.Project;
using Api.Infrastructure;
using Application.Calculations.GetByProject;
using Application.Project.AddMembers;
using Application.Project.AddMethods;
using Application.Project.AddParameters;
using Application.Project.Create;
using Application.Project.GetProjectById;
using Application.Project.GetProjects;
using Application.Project.GetUserProjects;
using Application.Project.Remove;
using Application.Project.RemoveMember;
using Application.Project.RemoveMethod;
using Application.Project.RemoveParameter;
using Carter;
using Mapster;
using MediatR;
using Shared;
using Shared.Results;
using GetProjectsResponse = Application.Project.GetProjects.GetProjectsResponse;

namespace Api.Endpoints;

public class ProjectEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("api/projects")
            .RequireAuthorization()
            .WithTags("projects")
            .WithOpenApi();

        group.MapPost("", MapPostCreateProject)
            .Produces<Guid>(201)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithSummary("Create project")
            .WithName("Create project");

        group.MapGet("", MapGetProjectList)
            .AllowAnonymous()
            .Produces<PaginatedList<GetProjectsResponse>>()
            .ProducesProblem(500)
            .WithSummary("Get projects paginated list")
            .WithName("Get projects");

        group.MapGet("{id:guid}", MapGetProjectById)
            .AllowAnonymous()
            .Produces<GetProjectByIdResponse>()
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Get project by id")
            .WithDescription("Get project");

        group.MapGet("mine", MapGetProjectByUser)
            .Produces<List<ProjectResponse>>()
            .ProducesProblem(401)
            .ProducesProblem(500)
            .WithName("Get projects list by authenticated user")
            .WithSummary("Get projects list by authenticated user");

        group.MapDelete("{id:guid}", MapDeleteProject)
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Delete project")
            .WithName("Delete project");

        group.MapGet("{id:guid}/calculations", MapGetProjectCalculations)
            .AllowAnonymous()
            .Produces<List<CalculationResponse>>(200)
            .ProducesProblem(500)
            .WithSummary("Get project calculations list")
            .WithName("Get project calculations");

        group.MapPost("{id:guid}/parameters", MapAddProjectParameters)
            .Produces(201)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Add project parameters")
            .WithName("Add project parameters");

        group.MapDelete("{projectId:guid}/parameters/{parameterId:guid}", MapDeleteProjectParameter)
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Remove project parameter")
            .WithName("Remove project parameter");

        group.MapPost("{projectId:guid}/methods", MapPostAddProjectMethods)
            .Produces(201)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Add project methods")
            .WithName("Add project methods");

        group.MapDelete("{projectId:guid}/methods/{methodId:guid}", MapDeleteProjectMethod)
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Remove project method")
            .WithName("Remove project method");

        group.MapPost("{projectId:guid}/members", MapPostAddProjectMembers)
            .Produces(201)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithSummary("Add project members")
            .WithName("Add project members");

        group.MapDelete("{projectId:guid}/members/{memberId:guid}", MapDeleteProjectMember)
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(401)
            .ProducesProblem(403)
            .ProducesProblem(500)
            .WithSummary("Remove project member")
            .WithName("Remove project member");
    }

    private static async Task<IResult> MapDeleteProjectMember(
        Guid projectId,
        Guid memberId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new RemoveProjectMemberCommand
        {
            MemberId = memberId,
            ProjectId = projectId
        };
        Result result = await sender.Send(command, cancellationToken);
        return result.Match(Results.NoContent, CustomResults.Problem);
    }

    private static async Task<IResult> MapPostAddProjectMembers(
        Guid projectId,
        List<Guid> memberIds,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new AddProjectMembersCommand
        {
            MemberIds = memberIds,
            ProjectId = projectId
        };
        Result result = await sender.Send(command, cancellationToken);
        return result.Match(Results.Created, CustomResults.Problem);
    }

    private static async Task<IResult> MapDeleteProjectMethod(
        Guid projectId,
        Guid methodId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new RemoveProjectMethodCommand
        {
            ProjectId = projectId,
            MethodId = methodId
        };
        Result result = await sender.Send(command, cancellationToken);
        return result.Match(Results.NoContent, CustomResults.Problem);
    }

    private static async Task<IResult> MapPostAddProjectMethods(
        Guid projectId,
        List<Guid> methodIds,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new AddProjectMethodsCommand
        {
            ProjectId = projectId,
            MethodIds = methodIds
        };
        Result result = await sender.Send(command, cancellationToken);
        return result.Match(Results.Created, CustomResults.Problem);
    }

    private static async Task<IResult> MapDeleteProjectParameter(
        Guid projectId,
        Guid parameterId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new RemoveProjectParameterCommand
        {
            ProjectId = projectId,
            ParameterId = parameterId
        };
        Result result = await sender.Send(command, cancellationToken);
        return result.Match(Results.NoContent, CustomResults.Problem);
    }

    private static async Task<IResult> MapAddProjectParameters(
        Guid id,
        List<AddProjectParametersRequest> parametersRequests,
        ISender sender,
        CancellationToken cancellationToken)
    {
        List<AddProjectParameter> parameters = parametersRequests.Adapt<List<AddProjectParameter>>();
        var command = new AddProjectParametersCommand
        {
            ProjectId = id,
            Parameters = parameters
        };
        Result result = await sender.Send(command, cancellationToken);
        return result.Match(Results.Created, CustomResults.Problem);
    }

    private static async Task<IResult> MapGetProjectCalculations(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetCalculationsByProjectQuery
        {
            ProjectId = id
        };

        Result<List<CalculationResponse>> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }

    private static async Task<IResult> MapDeleteProject(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new RemoveProjectCommand
        {
            ProjectId = id
        };
        Result result = await sender.Send(command, cancellationToken);
        return result.Match(Results.NoContent, CustomResults.Problem);
    }

    private static async Task<IResult> MapGetProjectByUser(ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetUserProjectsQuery
        {
        };
        Result<List<ProjectResponse>> result = await sender.Send(query, cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }

    private static async Task<IResult> MapGetProjectById(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetProjectByIdQuery
        {
            Id = id
        };
        Result<GetProjectByIdResponse> result = await sender.Send(query, cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }

    private static async Task<IResult> MapGetProjectList(
        [AsParameters] QueryParameters queryParameters,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetProjectsQuery
        {
            SortOrder = queryParameters.SortOrder,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            SortColumn = queryParameters.SortColumn,
            SearchTerm = queryParameters.SearchTerm
        };

        Result<PaginatedList<GetProjectsResponse>> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }

    private static async Task<IResult> MapPostCreateProject(
        CreateProjectRequest request,
        ISender sender,
        CancellationToken cancellationToken)
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
            Parameters = request.Parameters
                .Select(p => new AddProjectParameter
                {
                    PropertyId = p.PropertyId,
                    Value = p.Value
                })
                .ToList()
        };

        Result addParametersResult = await sender.Send(addParametersCommand, cancellationToken);

        return addParametersResult.Match(Results.Created, CustomResults.Problem);
    }
}
