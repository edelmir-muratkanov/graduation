using Application.Property.GetById;
using Domain.Properties;

namespace Infrastructure.Queries.Property;

/// <summary>
/// Обработчик запроса <see cref="GetPropertyByIdQuery"/>
/// </summary>
internal sealed class GetPropertyByIdQueryHandler(ApplicationReadDbContext context)
    : IQueryHandler<GetPropertyByIdQuery, GetPropertyByIdResponse>
{
    public async Task<Result<GetPropertyByIdResponse>> Handle(GetPropertyByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Запрос на получение свойства по идентификатору
        GetPropertyByIdResponse? property = await context.Properties
            .Select(p => new GetPropertyByIdResponse
            {
                Id = p.Id,
                Name = p.Name,
                Unit = p.Unit
            })
            .FirstOrDefaultAsync(p =>
                p.Id == request.Id, cancellationToken);

        // Если свойство не найдено, возвращаем ошибку
        return property ?? Result.Failure<GetPropertyByIdResponse>(PropertyErrors.NotFound);
    }
}
