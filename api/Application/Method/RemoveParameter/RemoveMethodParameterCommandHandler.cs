using Domain.Methods;

namespace Application.Method.RemoveParameter;

internal class RemoveMethodParameterCommandHandler(
    IMethodRepository methodRepository,
    IMethodParameterRepository methodParameterRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveMethodParameterCommand>
{
    public async Task<Result> Handle(RemoveMethodParameterCommand request, CancellationToken cancellationToken)
    {
        var method = await methodRepository.GetByIdAsync(request.MethodId, cancellationToken);

        if (method is null)
        {
            return Result.Failure(MethodErrors.NotFound);
        }

        method.RemoveParameter(request.ParameterId);

        methodRepository.Update(method);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}