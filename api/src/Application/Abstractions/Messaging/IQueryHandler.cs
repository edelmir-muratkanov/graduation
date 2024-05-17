namespace Application.Abstractions.Messaging;

/// <summary>
/// Интерфейс обработчика запроса <typeparamref name="TQuery"/> с возвращаемым результатом типа <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TQuery">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип возвращаемого результата.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
