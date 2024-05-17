namespace Application.Abstractions.Messaging;

/// <summary>
/// Интерфейс базовой команды.
/// </summary>
public interface IBaseCommand;

/// <summary>
/// Интерфейс команды без возвращаемого результата.
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand;

/// <summary>
/// Интерфейс команды с возвращаемым результатом типа <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TResponse">Тип возвращаемого результата.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;
