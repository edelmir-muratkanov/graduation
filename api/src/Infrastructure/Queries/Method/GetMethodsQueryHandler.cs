using System.Linq.Expressions;
using Application.Method.GetMethods;
using Shared.Mappings;

namespace Infrastructure.Queries.Method;

internal sealed class GetMethodsQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetMethodsQuery, PaginatedList<GetMethodsResponse>>
{
    public async Task<Result<PaginatedList<GetMethodsResponse>>> Handle(
        GetMethodsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<MethodReadModel>? methodsQuery = dbContext.Methods.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            methodsQuery = methodsQuery.Where(m =>
                EF.Functions.ILike(m.Name, $"%{request.SearchTerm}%"));
        }


        Expression<Func<MethodReadModel, object>> keySelector = request.SortColumn?.ToLower() switch
        {
            "name" => property => property.Name,
            _ => property => property.CreatedAt
        };

        methodsQuery = request.SortOrder == SortOrder.Desc
            ? methodsQuery.OrderByDescending(keySelector)
            : methodsQuery.OrderBy(keySelector);

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
