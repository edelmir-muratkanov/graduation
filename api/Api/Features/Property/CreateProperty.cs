using Api.Contracts.Property;
using Api.Domain.Property;
using Api.Infrastructure.Database;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Property;

public class CreatePropertyEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGroup("api/properties").MapPost("",
                async (CreatePropertyRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new CreateProperty.Command(request.Name, request.Unit);
                    var result = await sender.Send(command, cancellationToken);

                    return result.Match(Results.Ok, CustomResults.Problem);
                })
            .Produces<CreatePropertyResponse>()
            .ProducesProblem(404)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Create property")
            .WithTags("properties");
    }
}

public static class CreateProperty
{
    public record Response(Guid Id, string Name, string Unit);

    public record Command(string Name, string Unit) : ICommand<Response>;

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithErrorCode(PropertyErrorCodes.Create.MissingName)
                .MaximumLength(255).WithErrorCode(PropertyErrorCodes.Create.LongName);

            RuleFor(c => c.Unit)
                .NotEmpty().WithErrorCode(PropertyErrorCodes.Create.MissingUnit)
                .MaximumLength(255).WithErrorCode(PropertyErrorCodes.Create.LongUnit);
        }
    }

    internal sealed class Handler(ApplicationDbContext context) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (await context.Properties.AnyAsync(p => p.Name == request.Name, cancellationToken))
            {
                return Result.Failure<Response>(PropertyErrors.NameNotUnique);
            }

            var property = new Domain.Property.Property
            {
                Name = request.Name,
                Unit = request.Unit
            };

            context.Properties.Add(property);
            await context.SaveChangesAsync(cancellationToken);

            return new Response(property.Id, property.Name, property.Unit);
        }
    }
}