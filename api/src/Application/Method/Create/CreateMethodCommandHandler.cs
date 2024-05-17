using Domain.Methods;
using Domain.Properties;
using Mapster;

namespace Application.Method.Create;

/// <summary>
/// Обработчик команды <see cref="CreateMethodCommand"/>
/// </summary>
/// <param name="methodRepository">Репозиторий методов.</param>
/// <param name="methodParameterRepository">Репозиторий параметров методов.</param>
/// <param name="propertyRepository">Репозиторий свойств.</param>
/// <param name="unitOfWork">Репозиторий для сохранения изменений.</param>
internal class CreateMethodCommandHandler(
    IMethodRepository methodRepository,
    IMethodParameterRepository methodParameterRepository,
    IPropertyRepository propertyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMethodCommand>
{
    /// <inheritdoc />
    public async Task<Result> Handle(
        CreateMethodCommand request,
        CancellationToken cancellationToken)
    {
        // Проверка уникальности названия метода
        if (!await methodRepository.IsNameUniqueAsync(request.Name))
        {
            return Result.Failure(MethodErrors.NameNotUnique);
        }

        // Проверка существования свойств параметров
        foreach (CreateMethodParameterRequest? parameter in request.Parameters)
        {
            if (!await propertyRepository.Exists(parameter.PropertyId))
            {
                return Result.Failure(MethodParameterErrors.InvalidProperty);
            }
        }

        // Создание метода
        Result<Domain.Methods.Method>?
            methodResult = Domain.Methods.Method.Create(request.Name, request.CollectorTypes);

        if (methodResult.IsFailure)
        {
            return methodResult;
        }

        // Добавление параметров к методу
        var results = request
            .Parameters
            .Select(parameterRequest =>
            {
                ParameterValueGroup? first = parameterRequest.FirstParameters.Adapt<ParameterValueGroup>();
                ParameterValueGroup? second = parameterRequest.SecondParameters.Adapt<ParameterValueGroup>();
                return methodResult.Value.AddParameter(parameterRequest.PropertyId, first, second);
            }).ToList();

        // Проверка результатов добавления параметров
        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        // Сохранение метода и его параметров
        methodRepository.Insert(methodResult.Value);
        methodParameterRepository.InsertRange(methodResult.Value.Parameters);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
