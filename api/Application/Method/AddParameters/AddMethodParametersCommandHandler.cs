using Domain.Methods;
using Domain.Properties;

namespace Application.Method.AddParameters;

internal class AddMethodParametersCommandHandler(
    IMethodRepository methodRepository,
    IMethodParameterRepository methodParameterRepository,
    IPropertyRepository propertyRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddMethodParametersCommand>
{
    public async Task<Result> Handle(AddMethodParametersCommand request, CancellationToken cancellationToken)
    {
        var method = await methodRepository.GetByIdAsync(request.MethodId, cancellationToken);

        if (method is null) return Result.Failure(MethodErrors.NotFound);

        foreach (var parameter in request.Parameters)
            if (!await propertyRepository.Exists(parameter.PropertyId))
                return Result.Failure(MethodErrors.NotFoundParameter);

        var results = request.Parameters.Select(parameterRequest =>
                method.AddParameter(
                    parameterRequest.PropertyId,
                    parameterRequest.First,
                    parameterRequest.Second))
            .ToList();

        if (results.Any(r => r.IsFailure)) return Result.Failure(ValidationError.FromResults(results));

        methodParameterRepository.InsertRange(method.Parameters);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}