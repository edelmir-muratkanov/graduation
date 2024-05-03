using Domain.Calculation;
using Domain.Methods;
using Domain.Properties;

namespace Application.Method.RemoveParameter;

internal class RemoveMethodParameterCommandHandler(
    IMethodRepository methodRepository,
    ICalculationRepository calculationRepository,
    IPropertyRepository propertyRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveMethodParameterCommand>
{
    public async Task<Result> Handle(RemoveMethodParameterCommand request, CancellationToken cancellationToken)
    {
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(request.MethodId, cancellationToken);

        if (method is null)
        {
            return Result.Failure(MethodErrors.NotFound);
        }

        method.RemoveParameter(request.ParameterId);

        Domain.Properties.Property? property = await propertyRepository
            .GetByIdAsync(request.ParameterId, cancellationToken);

        List<Calculation> calculations = await calculationRepository
            .Get(calculation => calculation.MethodId == method.Id, cancellationToken);

        foreach (Calculation calculation in calculations)
        {
            calculation.RemoveItem(property!.Name);
            calculationRepository.Update(calculation);
        }

        methodRepository.Update(method);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
