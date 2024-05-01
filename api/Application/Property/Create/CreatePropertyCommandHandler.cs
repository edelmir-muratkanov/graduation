using Domain.Properties;

namespace Application.Property.Create;

internal sealed class CreatePropertyCommandHandler(
    IPropertyRepository propertyRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreatePropertyCommand, CreatePropertyResponse>
{
    public async Task<Result<CreatePropertyResponse>> Handle(CreatePropertyCommand request,
        CancellationToken cancellationToken)
    {
        if (!await propertyRepository.IsNameUniqueAsync(request.Name))
        {
            return Result.Failure<CreatePropertyResponse>(PropertyErrors.NameNotUnique);
        }

        Result<Domain.Properties.Property>? propertyResult =
            Domain.Properties.Property.Create(request.Name, request.Unit);

        if (propertyResult.IsFailure)
        {
            return Result.Failure<CreatePropertyResponse>(propertyResult.Error);
        }

        propertyRepository.Insert(propertyResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreatePropertyResponse(
            propertyResult.Value.Id,
            propertyResult.Value.Name,
            propertyResult.Value.Unit);
    }
}
