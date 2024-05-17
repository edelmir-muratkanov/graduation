using System.Linq.Expressions;
using Application.Project.GetProjects;
using Shared.Mappings;

namespace Infrastructure.Queries.Project;

/// <summary>
/// Обработчик запроса <see cref="GetProjectsQuery"/>
/// </summary>
internal sealed class GetProjectsQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetProjectsQuery, PaginatedList<GetProjectsResponse>>
{
    public async Task<Result<PaginatedList<GetProjectsResponse>>> Handle(
        GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        // Получение запроса для проектов из базы данных
        IQueryable<ProjectReadModel>? projectsQuery = dbContext.Projects.AsQueryable();

        // Применение фильтра по поисковому запросу
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            projectsQuery = projectsQuery.Where(m =>
                EF.Functions.ILike(m.Name, $"%{request.SearchTerm}%")
                || EF.Functions.ILike(m.Country, $"%{request.SearchTerm}%")
                || EF.Functions.ILike(m.Operator, $"%{request.SearchTerm}%"));
        }

        // Определение сортировки
        Expression<Func<ProjectReadModel, object>> keySelector = request.SortColumn?.ToLower() switch
        {
            "name" => property => property.Name,
            "country" => property => property.Country,
            "operator" => property => property.Operator,
            _ => property => property.CreatedAt
        };

        // Применение сортировки к запросу
        projectsQuery = request.SortOrder == SortOrder.Desc
            ? projectsQuery.OrderByDescending(keySelector)
            : projectsQuery.OrderBy(keySelector);

        // Получение списка проектов с пагинацией
        PaginatedList<GetProjectsResponse>? projects = await projectsQuery.AsSplitQuery()
            .Select(p =>
                new GetProjectsResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Country = p.Country,
                    Operator = p.Operator,
                    CollectorType = p.CollectorType,
                    Type = p.ProjectType
                })
            .PaginatedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);


        return projects;
    }
}
