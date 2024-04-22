using Domain.Methods;

namespace Application.Method.Update;

internal sealed class UpdateMethodCommandHandler(
    IMethodRepository methodRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateMethodCommand>
{
    public async Task<Result> Handle(UpdateMethodCommand request, CancellationToken cancellationToken)
    {
        var method = await methodRepository.GetByIdAsync(request.Id, cancellationToken);

        if (method is null) return Result.Failure(MethodErrors.NotFound);

        method.ChangeNameAndCollectorTypes(request.Name, request.CollectorTypes);

        methodRepository.Update(method);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}