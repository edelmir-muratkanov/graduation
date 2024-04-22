using Domain.Methods;
using Domain.Properties;
using Mapster;

namespace Application.Method.Create;

internal class CreateMethodCommandHandler(
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