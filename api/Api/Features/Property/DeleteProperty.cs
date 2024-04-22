using Api.Domain.Properties;
using Api.Domain.Users;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using MediatR;

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

    internal sealed class Handler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<DeletePropertyCommand>
    {
        public async Task<Result> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await propertyRepository.GetByIdAsync(request.Id, cancellationToken);

            if (property is null) return Result.Failure(PropertyErrors.NotFound);

            propertyRepository.Remove(property);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}