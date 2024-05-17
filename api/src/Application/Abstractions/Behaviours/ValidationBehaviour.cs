using System.Reflection;
using FluentValidation.Results;

namespace Application.Abstractions.Behaviours;

/// <summary>
/// Поведение для валидации запросов.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип ответа.</typeparam>
/// <param name="validators">Список валидаторов.</param>
public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Обрабатывает запрос и выполняет его валидацию.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="next">Делегат для вызова следующего обработчика в цепочке.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат обработки запроса.</returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ValidationFailure[] validationFailures = await ValidateAsync(request);

        if (validationFailures.Length == 0)
        {
            return await next();
        }

        if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            Type? resultType = typeof(TResponse).GetGenericArguments()[0];

            MethodInfo? failureMethod = typeof(Result<>)
                .MakeGenericType(resultType)
                .GetMethod(nameof(Result<object>.ValidationFailure));

            if (failureMethod is not null)
            {
                return ((TResponse)failureMethod.Invoke(
                    null,
                    new object[] { CreateValidationError(validationFailures) })!)!;
            }
        }
        else if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(CreateValidationError(validationFailures));
        }

        throw new ValidationException(validationFailures);
    }

    /// <summary>
    /// Выполняет валидацию запроса.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <returns>Массив ошибок валидации.</returns>
    private async Task<ValidationFailure[]> ValidateAsync(TRequest request)
    {
        if (!validators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TRequest>(request);

        ValidationResult[] validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context)));

        ValidationFailure[] validationFailures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();

        return validationFailures;
    }

    /// <summary>
    /// Создает объект ValidationError на основе списка ошибок валидации.
    /// </summary>
    /// <param name="validationFailures">Список ошибок валидации.</param>
    /// <returns>Объект <see cref="ValidationError"/>.</returns>
    private static ValidationError CreateValidationError(IEnumerable<ValidationFailure> validationFailures)
    {
        return new ValidationError(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage))
            .ToArray());
    }
}
