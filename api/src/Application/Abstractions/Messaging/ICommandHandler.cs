namespace Application.Abstractions.Messaging;

/// <summary>
/// Интерфейс обработчика команды без возвращаемого результата.
/// </summary>
/// <typeparam name="TCommand">Тип команды.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

/// <summary>
/// Интерфейс обработчика команды с возвращаемым результатом типа <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TCommand">Тип команды.</typeparam>
/// <typeparam name="TResponse">Тип возвращаемого результата.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;
