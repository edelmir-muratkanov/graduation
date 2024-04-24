using Domain.Methods;

namespace Application.Method.Delete;

internal sealed class DeleteMethodCommandHandler(IMethodRepository methodRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteMethodCommand>
{
    public async Task<Result> Handle(DeleteMethodCommand request, CancellationToken cancellationToken)
    {
        var method = await methodRepository.GetByIdAsync(request.Id, cancellationToken);

        if (method is null) return Result.Failure(MethodErrors.NotFound);

        methodRepository.Remove(method);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}