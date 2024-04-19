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

public class UpdatePropertyEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup("api/properties")
            .MapPatch("{id:guid}", async (
                Guid id,
                UpdatePropertyRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateProperty.UpdatePropertyCommand(id, request.Name, request.Unit);
                var result = await sender.Send(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithName("Update property")
            .WithTags("properties")
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(400)
            .ProducesProblem(500)
            .Produces(401)
            .Produces(403);
    }
}

public static class UpdateProperty
{
    public record UpdatePropertyCommand(Guid Id, string? Name, string? Unit) : ICommand;

    internal sealed class Validator : AbstractValidator<UpdatePropertyCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithErrorCode(PropertyErrorCodes.Update.MissingId);
        }
    }

    internal class Handler(ApplicationDbContext context) : ICommandHandler<UpdatePropertyCommand>
    {
        public async Task<Result> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await context.Properties
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

            if (property is null)
            {
                return Result.Failure(PropertyErrors.NotFound);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                property.Name = request.Name;
            }

            if (!string.IsNullOrWhiteSpace(request.Unit))
            {
                property.Unit = request.Unit;
            }

            context.Properties.Update(property);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}