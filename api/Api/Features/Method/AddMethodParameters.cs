using Api.Contracts.Method;
using Api.Domain.Methods;
using Api.Domain.Properties;
using Api.Domain.Users;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using Mapster;
using MediatR;

namespace Api.Features.Method;

public class AddMethodParametersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGroup("api/methods")
            .MapPost("{id:guid}/parameters", async (
                Guid id,
                List<AddMethodParametersRequest> parametersRequests,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var parameters = parametersRequests.Adapt<List<AddMethodParameters.MethodParameter>>();

                var command = new AddMethodParameters.AddMethodParametersCommand(id, parameters);

                var result = await sender.Send(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(409)
            .ProducesProblem(500)
            .WithName("Add method parameters")
            .WithTags("methods");
    }
}

public static class AddMethodParameters
{
    public record MethodParameter(
        Guid PropertyId,
        ParameterValueGroup? First,
        ParameterValueGroup? Second);

    public record AddMethodParametersCommand(Guid MethodId, List<MethodParameter> Parameters) : ICommand;

    internal class Validator : AbstractValidator<AddMethodParametersCommand>
    {
        public Validator()
        {
            RuleFor(c => c.MethodId)
                .NotEmpty();

            RuleFor(c => c.Parameters)
                .NotEmpty();

            RuleForEach(c => c.Parameters)
                .ChildRules(mp =>
                {
                    mp.RuleFor(p => p.PropertyId)
                        .NotEmpty();

                    mp.RuleFor(p => p.First)
                        .NotEmpty()
                        .When(p => p.Second is null);

                    mp.RuleFor(p => p.Second)
                        .NotEmpty()
                        .When(p => p.First is null);
                });
        }
    }

    internal class Handler(
        IMethodRepository methodRepository,
        IMethodParameterRepository methodParameterRepository,
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<AddMethodParametersCommand>
    {
        public async Task<Result> Handle(AddMethodParametersCommand request, CancellationToken cancellationToken)
        {
            var method = await methodRepository.GetByIdAsync(request.MethodId, cancellationToken);

            if (method is null)
            {
                return Result.Failure(MethodErrors.NotFound);
            }

            foreach (var parameter in request.Parameters)
            {
                if (!await propertyRepository.Exists(parameter.PropertyId))
                    return Result.Failure(MethodErrors.NotFoundParameter);
            }

            var results = request.Parameters.Select(parameterRequest =>
                    method.AddParameter(
                        parameterRequest.PropertyId,
                        parameterRequest.First,
                        parameterRequest.Second))
                .ToList();

            if (results.Any(r => r.IsFailure))
            {
                return Result.Failure(ValidationError.FromResults(results));
            }

            methodParameterRepository.InsertRange(method.Parameters);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}