using Domain.Properties;

namespace Application.Property.Delete;

internal sealed class DeletePropertyCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
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

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
