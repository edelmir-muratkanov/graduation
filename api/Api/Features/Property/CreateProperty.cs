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
                    var command = new CreateProperty.CreatePropertyCommand(request.Name, request.Unit);
                    var result = await sender.Send(command, cancellationToken);

                    return result.Match(Results.Created, CustomResults.Problem);
                })
            .Produces<CreatePropertyResponse>(201)
            .ProducesProblem(404)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Create property")
            .WithTags("properties");
    }
}

public static class CreateProperty
{
    public record CreatePropertyResponse(Guid Id, string Name, string Unit);

    public record CreatePropertyCommand(string Name, string Unit) : ICommand<CreatePropertyResponse>;

    internal sealed class Validator : AbstractValidator<CreatePropertyCommand>
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

    internal sealed class Handler(ApplicationDbContext context)
        : ICommandHandler<CreatePropertyCommand, CreatePropertyResponse>
    {
        public async Task<Result<CreatePropertyResponse>> Handle(CreatePropertyCommand request,
            CancellationToken cancellationToken)
        {
            if (await context.Properties.AnyAsync(p => p.Name == request.Name, cancellationToken))
            {
                return Result.Failure<CreatePropertyResponse>(PropertyErrors.NameNotUnique);
            }

            var property = new Domain.Property.Property
            {
                Name = request.Name,
                Unit = request.Unit
            };

            context.Properties.Add(property);
            await context.SaveChangesAsync(cancellationToken);

            return new CreatePropertyResponse(property.Id, property.Name, property.Unit);
        }
    }
}