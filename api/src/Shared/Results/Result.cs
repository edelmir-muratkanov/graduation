using System.Diagnostics.CodeAnalysis;

namespace Shared.Results;

/// <summary>
/// Представляет результат операции, который может быть успешным или с ошибкой.
/// </summary>
public class Result
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Result"/>.
    /// </summary>
    /// <param name="isSuccess">Флаг, указывающий успешность операции.</param>
    /// <param name="error">Объект ошибки, если результат является неуспешным.</param>
    /// <exception cref="ArgumentException">Выбрасывается, если указан недопустимый тип ошибки.</exception>
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Значение, указывающее, является ли результат успешным.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Значение, указывающее, является ли результат с ошибкой.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Объект ошибки, если результат является неуспешным.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Создает успешный результат.
    /// </summary>
    /// <returns>Успешный результат операции.</returns>
    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    /// <summary>
    /// Создает успешный результат с указанным значением.
    /// </summary>
    /// <typeparam name="TValue">Тип значения.</typeparam>
    /// <param name="value">Значение результата операции.</param>
    /// <returns>Успешный результат операции с указанным значением.</returns>
    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }

    /// <summary>
    /// Создает результат с ошибкой.
    /// </summary>
    /// <param name="error">Объект ошибки.</param>
    /// <returns>Результат операции с ошибкой.</returns>
    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    /// <summary>
    /// Создает результат с ошибкой для указанного типа значения.
    /// </summary>
    /// <typeparam name="TValue">Тип значения.</typeparam>
    /// <param name="error">Объект ошибки.</param>
    /// <returns>Результат операции с ошибкой для указанного типа значения.</returns>
    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default, false, error);
    }
}

/// <summary>
/// Представляет результат операции с возможным значением и ошибкой.
/// </summary>
/// <param name="value">Значение результата операции.</param>
/// <param name="isSuccess">Флаг, указывающий успешность операции.</param>
/// <param name="error">Объект ошибки, если результат является неуспешным.</param>
/// <typeparam name="TValue">Тип значения</typeparam>
public class Result<TValue>(TValue? value, bool isSuccess, Error error) : Result(isSuccess, error)
{
    /// <summary>
    /// Значение результата операции.
    /// </summary>
    /// <exception cref="InvalidOperationException">Выбрасывается, когда пытаемся получить значение результата с ошибкой</exception>
    [NotNull]
    public TValue Value => IsSuccess
        ? value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    /// <summary>
    /// Преобразует значение в результат операции с успешным или неуспешным статусом в зависимости от наличия значения.
    /// </summary>
    /// <param name="value">Значение результата операции.</param>
    /// <returns>Результат операции с успешным или неуспешным статусом в зависимости от наличия значения.</returns>
    public static implicit operator Result<TValue>(TValue? value)
    {
        return value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }

    /// <summary>
    /// Создает результат с ошибкой валидации.
    /// </summary>
    /// <param name="error">Объект ошибки.</param>
    /// <returns>Результат операции с ошибкой валидации.</returns>
    public static Result<TValue> ValidationFailure(Error error)
    {
        return new Result<TValue>(default, false, error);
    }
}
