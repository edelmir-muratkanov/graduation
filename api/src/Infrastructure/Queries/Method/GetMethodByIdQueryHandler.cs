using Application.Method.GetMethodById;
using Domain.Methods;

namespace Infrastructure.Queries.Method;

/// <summary>
/// Обработчик запроса <see cref="GetMethodByIdQuery"/>
/// </summary>
internal sealed class GetMethodByIdQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetMethodByIdQuery, GetMethodByIdResponse>
{
    public async Task<Result<GetMethodByIdResponse>> Handle(
        GetMethodByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Ищем метод в базе данных по указанному идентификатору
        GetMethodByIdResponse? method = await dbContext.Methods
            .Where(m => m.Id == request.Id)
            .Select(m =>
                new GetMethodByIdResponse
                {
                    Id = m.Id,
                    Name = m.Name,
                    CollectorTypes = m.CollectorTypes,
                    Parameters = m.Parameters.Select(p => new GetMethodByIdParameterResponse
                    {
                        Id = p.Id,
                        PropertyName = p.Property.Name,
                        PropertyUnit = p.Property.Unit,
                        First = p.FirstParameters,
                        Second = p.SecondParameters
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);

        // Возвращаем метод, если найден, иначе возвращаем ошибку о том, что метод не найден
        return method ?? Result.Failure<GetMethodByIdResponse>(MethodErrors.NotFound);
    }
}
