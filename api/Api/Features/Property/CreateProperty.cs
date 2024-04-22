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
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(201)
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

    internal sealed class Handler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<CreatePropertyCommand, CreatePropertyResponse>
    {
        public async Task<Result<CreatePropertyResponse>> Handle(CreatePropertyCommand request,
            CancellationToken cancellationToken)
        {
            if (!await propertyRepository.IsNameUniqueAsync(request.Name))
                return Result.Failure<CreatePropertyResponse>(PropertyErrors.NameNotUnique);

            var propertyResult = Domain.Properties.Property.Create(request.Name, request.Unit);

            if (propertyResult.IsFailure)
            {
                return Result.Failure<CreatePropertyResponse>(propertyResult.Error);
            }

            propertyRepository.Insert(propertyResult.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreatePropertyResponse(
                propertyResult.Value.Id,
                propertyResult.Value.Name,
                propertyResult.Value.Unit);
        }
    }
}