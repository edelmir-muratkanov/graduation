using Application.Project.GetProjectById;
using Domain.Projects;

namespace Infrastructure.Queries.Project;

/// <summary>
/// Обработчик запроса <see cref="GetProjectByIdQuery"/>
/// </summary>
internal sealed class GetProjectByIdQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetProjectByIdQuery, GetProjectByIdResponse>
{
    public async Task<Result<GetProjectByIdResponse>> Handle(GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Поиск проекта в базе данных по его идентификатору
        GetProjectByIdResponse? project = await dbContext.Projects
            .Where(p => p.Id == request.Id)
            .Select(p => new GetProjectByIdResponse
            {
                Id = p.Id,
                Name = p.Name,
                Country = p.Country,
                Operator = p.Operator,
                Type = p.ProjectType,
                CollectorType = p.CollectorType,
                OwnerId = Guid.Parse(p.CreatedBy!),
                Methods = p.Methods.Select(m => new GetProjectByIdMethod
                {
                    Id = m.Method.Id,
                    Name = m.Method.Name
                }).ToList(),
                Members = p.Members.Select(m => new GetProjectByIdMember
                {
                    Id = m.Member.Id,
                    Email = m.Member.Email
                }).ToList(),
                Parameters = p.Parameters.Select(pp => new GetProjectByIdParameter
                {
                    Id = pp.Id,
                    Name = pp.Property.Name,
                    Unit = pp.Property.Unit,
                    Value = pp.Value
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        // Возвращаем результат операции
        return project ?? Result.Failure<GetProjectByIdResponse>(ProjectErrors.NotFound);
    }
}
