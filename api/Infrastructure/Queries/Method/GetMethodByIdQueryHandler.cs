using Application.Method.GetMethodById;
using Domain.Methods;

namespace Infrastructure.Queries.Method;

internal sealed class GetMethodByIdQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetMethodByIdQuery, GetMethodByIdResponse>
{
    public async Task<Result<GetMethodByIdResponse>> Handle(
        GetMethodByIdQuery request,
        CancellationToken cancellationToken)
    {
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

        return method ?? Result.Failure<GetMethodByIdResponse>(MethodErrors.NotFound);
    }
}
