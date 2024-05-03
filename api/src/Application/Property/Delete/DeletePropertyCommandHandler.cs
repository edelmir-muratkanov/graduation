using Domain.Calculation;
using Domain.Properties;

namespace Application.Property.Delete;

internal sealed class DeletePropertyCommandHandler(
    IPropertyRepository propertyRepository,
    ICalculationRepository calculationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePropertyCommand>
{
    public async Task<Result> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        Domain.Properties.Property? property = await propertyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (property is null)
        {
            return Result.Failure(PropertyErrors.NotFound);
        }

        propertyRepository.Remove(property);

        List<Calculation> calculations = await calculationRepository.Get(null, cancellationToken);

        foreach (Calculation calculation in calculations)
        {
            calculation.RemoveItem(property.Name);
            calculationRepository.Update(calculation);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
