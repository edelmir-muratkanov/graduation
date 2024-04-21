using Api.Contracts.Method;
using Api.Domain;
using Api.Domain.Methods;
using Api.Domain.Properties;
using Api.Domain.Users;
using Api.Infrastructure.Database;
using Api.Shared.Interfaces;
using Api.Shared.Messaging;
using Api.Shared.Models;
using Carter;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Method;

public class CreateMethodEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGroup("api/methods").MapPost("", async (CreateMethodRequest request, ISender sender, IMapper mapper) =>
            {
                var parameters = request.Parameters.Select(p =>
                {
                    var first = p.First is not null
                        ? new CreateMethod.CreateMethodParameterValueGroupRequest(p.First.Min, p.First.Avg, p.First.Max)
                        : default;

                    var second = p.Second is not null
                        ? new CreateMethod.CreateMethodParameterValueGroupRequest(
                            p.Second.Min,
                            p.Second.Avg,
                            p.Second.Max)
                        : default;

                    return new CreateMethod.CreateMethodParameterRequest(p.PropertyId, first, second);
                }).ToList();
                var command =
                    new CreateMethod.CreateMethodCommand(request.Name, request.CollectorTypes, parameters);

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
    public record CreateMethodResponse(Guid Id, string Name);

    public record CreateMethodParameterValueGroupRequest(double Min, double Avg, double Max);

    public record CreateMethodParameterRequest(
        Guid PropertyId,
        CreateMethodParameterValueGroupRequest? FirstParameters,
        CreateMethodParameterValueGroupRequest? SecondParameters);

    public record CreateMethodCommand(
        string Name,
        HashSet<CollectorType> CollectorTypes,
        List<CreateMethodParameterRequest> Parameters)
        : ICommand<CreateMethodResponse>;

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

    // internal sealed class CreateMethodParameterValidator : AbstractValidator<CreateMethodParameterRequest>
    // {
    //     public CreateMethodParameterValidator()
    //     {
    //         RuleFor(c => c.PropertyId)
    //             .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingProperty);
    //
    //         RuleFor(c => c.FirstParameters)
    //             .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingFirstOrSecondParameters)
    //             .When(c => c.SecondParameters == null);
    //
    //         RuleFor(c => c.SecondParameters)
    //             .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingFirstOrSecondParameters)
    //             .When(c => c.FirstParameters == default);
    //     }
    // }

    internal class Handler(
        IMethodRepository methodRepository,
        IMethodParameterRepository methodParameterRepository,
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<CreateMethodCommand, CreateMethodResponse>
    {
        public async Task<Result<CreateMethodResponse>> Handle(
            CreateMethodCommand request,
            CancellationToken cancellationToken)
        {
            if (!await methodRepository.IsNameUniqueAsync(request.Name))
            {
                return Result.Failure<CreateMethodResponse>(MethodErrors.NameNotUnique);
            }

            var method = new Domain.Methods.Method
            {
                Name = request.Name,
                CollectorTypes = request.CollectorTypes.ToList()
            };


            List<MethodParameter> parameters = [];

            foreach (var parameter in request.Parameters)
            {
                if (!await propertyRepository.Exists(parameter.PropertyId))
                {
                    return Result.Failure<CreateMethodResponse>(MethodErrors.InvalidProperty);
                }


                var p = new MethodParameter
                {
                    MethodId = method.Id,
                    PropertyId = parameter.PropertyId,
                    FirstParameters = parameter.FirstParameters is not null
                        ? new ParameterValueGroup(parameter.FirstParameters.Min,
                            parameter.FirstParameters.Avg, parameter.FirstParameters.Max)
                        : default,
                    SecondParameters = parameter.SecondParameters is not null
                        ? new ParameterValueGroup(parameter.SecondParameters.Min,
                            parameter.SecondParameters.Avg, parameter.SecondParameters.Max)
                        : default
                };

                parameters.Add(p);
            }

            methodRepository.Insert(method);
            methodParameterRepository.InsertRange(parameters);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateMethodResponse(method.Id, method.Name);
        }
    }
}