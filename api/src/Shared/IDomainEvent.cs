using MediatR;

namespace Shared;

/// <summary>
/// Интерфейс для всех событий домена.
/// </summary>
/// <remarks>
/// Наследуется от <see cref="INotification"/> библиотеки MediatR, что позволяет использовать 
/// данный интерфейс для публикации событий и обработки их соответствующими обработчиками.
/// </remarks>
public interface IDomainEvent : INotification;
