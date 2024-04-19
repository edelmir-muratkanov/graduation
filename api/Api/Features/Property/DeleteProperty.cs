using Api.Domain.Property;
using Api.Domain.Users;
using Api.Infrastructure.Database;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Property;

public class DeletePropertyEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup("api/properties")
            .MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new DeleteProperty.DeletePropertyCommand(id);
                var result = await sender.Send(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Delete property")
            .WithTags("properties");
    }
}

public static class DeleteProperty
{
    public record DeletePropertyCommand(Guid Id) : ICommand;

    internal sealed class Validator : AbstractValidator<DeletePropertyCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithErrorCode(PropertyErrorCodes.Delete.MissingId);
        }
    }

    internal sealed class Handler(ApplicationDbContext context) : ICommandHandler<DeletePropertyCommand>
    {
        public async Task<Result> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await context.Properties.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (property is null)
            {
                return Result.Failure(PropertyErrors.NotFound);
            }

            context.Properties.Remove(property);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}