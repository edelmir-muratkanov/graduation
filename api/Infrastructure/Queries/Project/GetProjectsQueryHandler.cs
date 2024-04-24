using System.Linq.Expressions;
using Application.Abstractions.Messaging;
using Application.Project.GetProjects;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Shared;
using Shared.Mappings;
using Shared.Results;

namespace Infrastructure.Queries.Project;

internal class GetProjectsQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetProjectsQuery, PaginatedList<GetProjectsResponse>>
{
    public async Task<Result<PaginatedList<GetProjectsResponse>>> Handle(
        GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var projectsQuery = dbContext.Projects.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            projectsQuery = projectsQuery.Where(m =>
                m.Name.Contains(request.SearchTerm)
                || m.Country.Contains(request.SearchTerm)
                || m.Operator.Contains(request.SearchTerm));


        Expression<Func<ProjectReadModel, object>> keySelector = request.SortColumn?.ToLower() switch
        {
            "name" => property => property.Name,
            "country" => property => property.Country,
            "operator" => property => property.Operator,
            _ => property => property.CreatedAt
        };

        projectsQuery = request.SortOrder == SortOrder.Asc
            ? projectsQuery.OrderBy(keySelector)
            : projectsQuery.OrderByDescending(keySelector);

        var projects = await projectsQuery.AsSplitQuery()
            .Select(p =>
                new GetProjectsResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Country = p.Country,
                    Operator = p.Operator,
                    CollectorType = p.CollectorType.ToString(),
                    Type = p.ProjectType.ToString()
                }
            ).PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);


        return projects;
    }
}