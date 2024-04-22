using Api.Contracts.Property;
using Api.Domain.Properties;
using Api.Domain.Users;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using MediatR;

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
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .Produces(401)
            .Produces(403)
            .ProducesProblem(404)
            .ProducesProblem(500)
            .WithName("Update property")
            .WithTags("properties");
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

    internal class Handler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<UpdatePropertyCommand>
    {
        public async Task<Result> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await propertyRepository.GetByIdAsync(request.Id, cancellationToken);

            if (property is null) return Result.Failure(PropertyErrors.NotFound);

            var propertyResult = property.Update(request.Name, request.Unit);

            if (propertyResult.IsFailure)
            {
                return propertyResult;
            }

            propertyRepository.Update(property);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}