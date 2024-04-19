using Api.Infrastructure.Database;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Property;

public class GetPropertiesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup("api/properties")
            .MapGet("", async (string? searchTerm, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetProperties.Query(searchTerm);
                var result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces<PaginatedList<GetProperties.Response>>()
            .Produces(500)
            .WithName("Get properties")
            .WithTags("properties");
    }
}

public static class GetProperties
{
    public record Response(Guid Id, string Name, string Unit);

    public record Query(string? SearchTerm, int PageNumber = 1, int PageSize = 10) : IQuery<PaginatedList<Response>>;

    internal sealed class Handler(ApplicationDbContext context) : IQueryHandler<Query, PaginatedList<Response>>
    {
        public async Task<Result<PaginatedList<Response>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var propertiesQuery = context.Properties.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                propertiesQuery = propertiesQuery.Where(p => p.Name.Contains(request.SearchTerm));
            }

            var properties = propertiesQuery.Select(p => new Response(p.Id, p.Name, p.Unit));

            var list = await PaginatedList<Response>.CreateAsync(
                properties,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            return list;
        }
    }
}