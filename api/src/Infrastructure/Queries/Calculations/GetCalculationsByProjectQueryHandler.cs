using Application.Calculations.GetByProject;

namespace Infrastructure.Queries.Calculations;

/// <summary>
/// Обработчик запроса <see cref="GetCalculationsByProjectQuery"/>
/// </summary>
internal sealed class GetCalculationsByProjectQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetCalculationsByProjectQuery, List<CalculationResponse>>
{
    public async Task<Result<List<CalculationResponse>>> Handle(
        GetCalculationsByProjectQuery request,
        CancellationToken cancellationToken)
    {
        // Получаем расчеты для указанного идентификатора проекта, включая связанные элементы расчета
        return await dbContext.Calculations
            .Where(c => c.ProjectId == request.ProjectId)
            .Include(c => c.Items)
            .Select(c =>
                new CalculationResponse
                {
                    Name = c.Method.Name,
                    Ratio = c.Belonging!.Degree,
                    Applicability = c.Belonging!.Status,
                    Items = c.Items.Select(i => new CalculationItemResponse
                    {
                        Name = i.PropertyName,
                        Ratio = i.Belonging.Degree
                    }).ToList()
                })
            .ToListAsync(cancellationToken);
    }
}
