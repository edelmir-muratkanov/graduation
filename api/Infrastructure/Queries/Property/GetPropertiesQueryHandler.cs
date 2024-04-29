using System.Linq.Expressions;
using Application.Property.Get;

namespace Infrastructure.Queries.Property;

internal sealed class GetPropertiesQueryHandler(ApplicationReadDbContext context)
    : IQueryHandler<GetPropertiesQuery, PaginatedList<GetPropertiesResponse>>
{
    public async Task<Result<PaginatedList<GetPropertiesResponse>>> Handle(GetPropertiesQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<PropertyReadModel>? propertiesQuery = context.Properties.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            propertiesQuery = propertiesQuery.Where(p =>
                EF.Functions.ILike(p.Name, $"%{request.SearchTerm}%"));
        }

        Expression<Func<PropertyReadModel, object>> keySelector = request.SortColumn?.ToLower() switch
        {
            "name" => property => property.Name,
            "unit" => property => property.Unit,
            _ => property => property.CreatedAt
        };

        propertiesQuery = request.SortOrder == SortOrder.Desc
            ? propertiesQuery.OrderByDescending(keySelector)
            : propertiesQuery.OrderBy(keySelector);

        IQueryable<GetPropertiesResponse>? properties = propertiesQuery.Select(p =>
            new GetPropertiesResponse(p.Id, p.Name, p.Unit));

        var list = await PaginatedList<GetPropertiesResponse>.CreateAsync(
            properties,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return list;
    }
}
