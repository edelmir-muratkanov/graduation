using Api.Domain.Property;
using Api.Infrastructure.Database;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Property;

public class GetPropertyByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup("api/properties")
            .MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetPropertyById.GetPropertyByIdQuery(id);
                var result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithName("Get property by id")
            .WithTags("properties");
    }
}

public static class GetPropertyById
{
    public record GetPropertyByIdResponse(Guid Id, string Name, string Unit);

    public record GetPropertyByIdQuery(Guid Id) : IQuery<GetPropertyByIdResponse>;

    internal class Validator : AbstractValidator<GetPropertyByIdQuery>
    {
        public Validator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithErrorCode(PropertyErrorCodes.GetById.MissingId);
        }
    }

    internal class Handler(ApplicationDbContext context) : IQueryHandler<GetPropertyByIdQuery, GetPropertyByIdResponse>
    {
        public async Task<Result<GetPropertyByIdResponse>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await context.Properties.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            return property is null
                ? Result.Failure<GetPropertyByIdResponse>(PropertyErrors.NotFound)
                : new GetPropertyByIdResponse(property.Id, property.Name, property.Unit);
        }
    }
}