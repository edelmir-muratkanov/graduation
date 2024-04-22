using Api.Contracts.Method;
using Api.Domain;
using Api.Domain.Methods;
using Api.Domain.Properties;
using Api.Domain.Users;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Api.Features.Method;

public class CreateMethodEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGroup("api/methods").MapPost("",
                async (CreateMethodRequest request, ISender sender, IMapper mapper) =>
                {
                    var parameters = request.Parameters.Select(p =>
                    {
                        var first = p.First is not null
                            ? new CreateMethod.CreateMethodParameterValueGroupRequest(p.First.Min, p.First.Avg,
                                p.First.Max)
                            : default;

                        var second = p.Second is not null
                            ? new CreateMethod.CreateMethodParameterValueGroupRequest(
                                p.Second.Min,
                                p.Second.Avg,
                                p.Second.Max)
                            : default;

                        return new CreateMethod.CreateMethodParameterRequest(p.PropertyId, first, second);
                    }).ToList();

                    var command = new CreateMethod.CreateMethodCommand(
                        request.Name,
                        request.CollectorTypes,
                        parameters);

                    var result = await sender.Send(command);
                    return result.Match(Results.Created, CustomResults.Problem);
                })
            .RequireAuthorization(Role.Admin.ToString())
            .Produces(201)
            .ProducesProblem(400)
            .Produces(401)
            .Produces(403)
            .ProducesProblem(409)
            .Produces(500)
            .WithName("Create method")
            .WithTags("methods");
    }
}

public static class CreateMethod
{
    public record CreateMethodParameterValueGroupRequest(double Min, double Avg, double Max);

    public record CreateMethodParameterRequest(
        Guid PropertyId,
        CreateMethodParameterValueGroupRequest? FirstParameters,
        CreateMethodParameterValueGroupRequest? SecondParameters);

    public record CreateMethodCommand(
        string Name,
        List<CollectorType> CollectorTypes,
        List<CreateMethodParameterRequest> Parameters)
        : ICommand;

    internal sealed class CreateMethodCommandValidator : AbstractValidator<CreateMethodCommand>
    {
        public CreateMethodCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingName)
                .MaximumLength(255).WithErrorCode(MethodErrorCodes.Create.LongName);

            RuleFor(c => c.CollectorTypes)
                .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingCollectorTypes)
                .ForEach(ct =>
                    ct.IsInEnum().WithErrorCode(MethodErrorCodes.Create.InvalidCollectorType));

            RuleFor(c => c.Parameters)
                .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingParameters)
                .ForEach(mp => mp
                    .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingParameter));

            RuleForEach(c => c.Parameters)
                .ChildRules(mp =>
                {
                    mp.RuleFor(p => p.PropertyId)
                        .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingProperty);

                    mp.RuleFor(p => p.FirstParameters)
                        .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingFirstOrSecondParameters)
                        .When(p => p.SecondParameters is null);

                    mp.RuleFor(p => p.SecondParameters)
                        .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingFirstOrSecondParameters)
                        .When(p => p.FirstParameters is null);
                });
        }
    }

    internal class Handler(
        IMethodRepository methodRepository,
        IMethodParameterRepository methodParameterRepository,
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<CreateMethodCommand>
    {
        public async Task<Result> Handle(
            CreateMethodCommand request,
            CancellationToken cancellationToken)
        {
            if (!await methodRepository.IsNameUniqueAsync(request.Name))
                return Result.Failure(MethodErrors.NameNotUnique);

            foreach (var parameter in request.Parameters)
                if (!await propertyRepository.Exists(parameter.PropertyId))
                    return Result.Failure(MethodParameterErrors.InvalidProperty);

            var methodResult = Domain.Methods.Method.Create(request.Name, request.CollectorTypes);

            if (methodResult.IsFailure) return methodResult;

            var results = request
                .Parameters
                .Select(parameterRequest =>
                {
                    var first = parameterRequest.FirstParameters.Adapt<ParameterValueGroup>();
                    var second = parameterRequest.SecondParameters.Adapt<ParameterValueGroup>();
                    return methodResult.Value.AddParameter(parameterRequest.PropertyId, first, second);
                }).ToList();

            if (results.Any(r => r.IsFailure)) return Result.Failure(ValidationError.FromResults(results));


            methodRepository.Insert(methodResult.Value);
            methodParameterRepository.InsertRange(methodResult.Value.Parameters);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}