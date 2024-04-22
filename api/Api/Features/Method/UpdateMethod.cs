using System.Text.Json;
using Api.Contracts.Method;
using Api.Domain;
using Api.Domain.Methods;
using Api.Domain.Users;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using Mapster;
using MediatR;

namespace Api.Features.Method;

public class UpdateMethodEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGroup("api/methods")
            .MapPatch("{id:guid}", async (
                Guid id,
                UpdateMethodRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateMethod.UpdateMethodCommand(
                    id,
                    request.Name,
                    request.CollectorTypes);

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
            .WithName("Update method")
            .WithTags("methods");
    }
}

public static class UpdateMethod
{
    public record UpdateMethodCommand(
        Guid Id,
        string? Name,
        List<CollectorType>? CollectorTypes) : ICommand;


    internal sealed class Validator : AbstractValidator<UpdateMethodCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithErrorCode(MethodErrorCodes.Update.MissingId);

            RuleFor(c => c.Name)
                .MaximumLength(255).WithErrorCode(MethodErrorCodes.Update.LongName);

            RuleFor(c => c.CollectorTypes)
                .ForEach(ct =>
                    ct.IsInEnum().WithErrorCode(MethodErrorCodes.Update.InvalidCollectorType));
        }
    }


    internal sealed class Handler(IMethodRepository methodRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateMethodCommand>
    {
        public async Task<Result> Handle(UpdateMethodCommand request, CancellationToken cancellationToken)
        {
            var method = await methodRepository.GetByIdAsync(request.Id, cancellationToken);

            if (method is null) return Result.Failure(MethodErrors.NotFound);

            method.ChangeNameAndCollectorTypes(request.Name, request.CollectorTypes);

            methodRepository.Update(method);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}