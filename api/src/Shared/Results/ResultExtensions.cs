namespace Shared.Results;

/// <summary>
/// Содержит методы расширения для работы с объектами <see cref="Result"/> и <see cref="Result{TIn}"/>.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Выполняет указанный делегат в зависимости от успешности результата.
    /// </summary>
    /// <param name="result">Результат операции.</param>
    /// <param name="onSuccess">Делегат, который будет выполнен в случае успеха.</param>
    /// <param name="onFailure">Делегат, который будет выполнен в случае ошибки.</param>
    /// <typeparam name="TOut">Тип возвращаемого значения.</typeparam>
    /// <returns>Результат выполнения делегата в зависимости от успешности операции.</returns>
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    /// <summary>
    /// Выполняет указанный делегат в зависимости от успешности результата.
    /// </summary>
    /// <param name="result">Результат операции.</param>
    /// <param name="onSuccess">Делегат, который будет выполнен в случае успеха.</param>
    /// <param name="onFailure">Делегат, который будет выполнен в случае ошибки.</param>
    /// <typeparam name="TIn">Тип значения результата.</typeparam>
    /// <typeparam name="TOut">Тип возвращаемого значения.</typeparam>
    /// <returns></returns>
    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}
