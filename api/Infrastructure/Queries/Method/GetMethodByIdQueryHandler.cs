using Application.Abstractions.Messaging;
using Application.Method.GetMethodById;
using Domain.Methods;
using Infrastructure.Database;
using Shared.Results;

namespace Infrastructure.Queries.Method;

internal sealed class GetMethodByIdQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetMethodByIdQuery, GetMethodByIdResponse>
{
    public async Task<Result<GetMethodByIdResponse>> Handle(
        GetMethodByIdQuery request,
        CancellationToken cancellationToken)
    {
        var method = await dbContext.Methods.Select(m => new GetMethodByIdResponse
        {
            Id = m.Id,
            Name = m.Name,
            CollectorTypes = m.CollectorTypes,
            Parameters = m.Parameters.Select(p => new GetMethodByIdParameterResponse
            {
                Id = p.Id,
                PropertyName = p.Property.Name,
                First = p.FirstParameters,
                Second = p.SecondParameters
            }).ToList()
        }).FirstOrDefaultAsync(cancellationToken);

        return method ?? Result.Failure<GetMethodByIdResponse>(MethodErrors.NotFound);
    }
}