using Domain.Methods;

namespace Application.Method.Delete;

/// <summary>
/// Обработчик команды <see cref="DeleteMethodCommand"/>
/// </summary>
internal sealed class DeleteMethodCommandHandler(IMethodRepository methodRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteMethodCommand>
{
    /// <summary>
    /// Обработка команды удаления метода.
    /// </summary>
    /// <param name="request">Команда для удаления метода.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат выполнения команды.</returns>
    public async Task<Result> Handle(DeleteMethodCommand request, CancellationToken cancellationToken)
    {
        // Получение метода по его идентификатору
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(request.Id, cancellationToken);

        if (method is null)
        {
            return Result.Failure(MethodErrors.NotFound);
        }

        // Удаление метода
        methodRepository.Remove(method);
        
        // Сохранение изменений в базе данных
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
