using System.Linq.Expressions;
using Application.Method.GetMethods;
using Shared.Mappings;

namespace Infrastructure.Queries.Method;

/// <summary>
/// Обработчик запроса <see cref="GetMethodsQuery"/>
/// </summary>
internal sealed class GetMethodsQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetMethodsQuery, PaginatedList<GetMethodsResponse>>
{
    public async Task<Result<PaginatedList<GetMethodsResponse>>> Handle(
        GetMethodsQuery request,
        CancellationToken cancellationToken)
    {
        // Начальный запрос для получения всех методов из базы данных
        IQueryable<MethodReadModel>? methodsQuery = dbContext.Methods.AsQueryable();

        // Применяем фильтрацию по поисковому запросу, если он указан
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            methodsQuery = methodsQuery.Where(m =>
                EF.Functions.ILike(m.Name, $"%{request.SearchTerm}%"));
        }

        // Определяем селектор ключа сортировки в зависимости от указанного столбца сортировки
        Expression<Func<MethodReadModel, object>> keySelector = request.SortColumn?.ToLower() switch
        {
            "name" => property => property.Name,
            _ => property => property.CreatedAt
        };

        // Применяем сортировку в соответствии с выбранным порядком сортировки
        methodsQuery = request.SortOrder == SortOrder.Desc
            ? methodsQuery.OrderByDescending(keySelector)
            : methodsQuery.OrderBy(keySelector);

        // Выполняем запрос для получения пагинированного списка методов
        PaginatedList<GetMethodsResponse>? methods = await methodsQuery.AsSplitQuery()
            .Select(m =>
                new GetMethodsResponse
                {
                    Id = m.Id,
                    Name = m.Name,
                    CollectorTypes = m.CollectorTypes
                }
            ).PaginatedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);


        return methods;
    }
}
