using System.Linq.Expressions;
using Application.Property.Get;
using Shared.Mappings;

namespace Infrastructure.Queries.Property;

/// <summary>
/// Обработчик запроса <see cref="GetPropertiesQuery"/>
/// </summary>
internal sealed class GetPropertiesQueryHandler(ApplicationReadDbContext context)
    : IQueryHandler<GetPropertiesQuery, PaginatedList<GetPropertiesResponse>>
{
    public async Task<Result<PaginatedList<GetPropertiesResponse>>> Handle(GetPropertiesQuery request,
        CancellationToken cancellationToken)
    {
        // Запрос на получение списка свойств
        IQueryable<PropertyReadModel>? propertiesQuery = context.Properties.AsQueryable();

        // Фильтрация по поисковому запросу
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            propertiesQuery = propertiesQuery.Where(p =>
                EF.Functions.ILike(p.Name, $"%{request.SearchTerm}%"));
        }

        // Сортировка
        Expression<Func<PropertyReadModel, object>> keySelector = request.SortColumn?.ToLower() switch
        {
            "name" => property => property.Name,
            "unit" => property => property.Unit,
            _ => property => property.CreatedAt
        };

        propertiesQuery = request.SortOrder == SortOrder.Desc
            ? propertiesQuery.OrderByDescending(keySelector)
            : propertiesQuery.OrderBy(keySelector);

        // Выполнение запроса и преобразование результатов
        PaginatedList<GetPropertiesResponse> properties = await propertiesQuery
            .Select(p => new GetPropertiesResponse(
                p.Id,
                p.Name,
                p.Unit))
            .PaginatedListAsync(
                request.PageNumber ?? 1,
                request.PageSize ?? 10,
                cancellationToken);


        return properties;
    }
}
