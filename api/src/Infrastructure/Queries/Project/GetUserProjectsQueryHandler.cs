using Application.Abstractions.Authentication;
using Application.Project.GetUserProjects;

namespace Infrastructure.Queries.Project;

internal sealed class GetUserProjectsQueryHandler(
    ApplicationReadDbContext dbContext,
    ICurrentUserService currentUserService)
    : IQueryHandler<GetUserProjectsQuery, List<ProjectResponse>>
{
    public async Task<Result<List<ProjectResponse>>> Handle(GetUserProjectsQuery request,
        CancellationToken cancellationToken)
    {
        List<ProjectResponse> projects = await dbContext.Projects
            .Where(p =>
                p.CreatedBy == currentUserService.Id ||
                p.Members.Any(pm => pm.MemberId.ToString() == currentUserService.Id))
            .Select(p => new ProjectResponse
            {
                Id = p.Id,
                Name = p.Name,
                Country = p.Country,
                Operator = p.Operator,
                Type = p.ProjectType,
                CollectorType = p.CollectorType
            })
            .ToListAsync(cancellationToken);

        return projects;
    }
}
