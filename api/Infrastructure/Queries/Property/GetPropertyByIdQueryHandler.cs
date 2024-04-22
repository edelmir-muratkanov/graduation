using Application.Abstractions.Messaging;
using Application.Property.GetById;
using Domain.Properties;
using Infrastructure.Database;
using Shared.Results;

namespace Infrastructure.Queries.Property;

internal class GetPropertyByIdQueryHandler(ApplicationReadDbContext context)
    : IQueryHandler<GetPropertyByIdQuery, GetPropertyByIdResponse>
{
    public async Task<Result<GetPropertyByIdResponse>> Handle(GetPropertyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var property = await context.Properties
            .Select(p => new GetPropertyByIdResponse
            {
                Id = p.Id,
                Name = p.Name,
                Unit = p.Unit
            })
            .FirstOrDefaultAsync(p =>
                p.Id == request.Id, cancellationToken);

        return property ?? Result.Failure<GetPropertyByIdResponse>(PropertyErrors.NotFound);
    }
}