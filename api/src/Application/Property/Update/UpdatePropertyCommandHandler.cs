using Domain.Properties;

namespace Application.Property.Update;

internal class UpdatePropertyCommandHandler(
    IPropertyRepository propertyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePropertyCommand>
{
    public async Task<Result> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        Domain.Properties.Property? property = await propertyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (property is null)
        {
            return Result.Failure(PropertyErrors.NotFound);
        }

        Result? propertyResult = property.Update(request.Name, request.Unit);

        if (propertyResult.IsFailure)
        {
            return propertyResult;
        }

        propertyRepository.Update(property);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
