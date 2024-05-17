namespace Application.Abstractions.Messaging;

/// <summary>
/// Интерфейс запроса с возвращаемым результатом типа <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TResponse">Тип возвращаемого результата.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
