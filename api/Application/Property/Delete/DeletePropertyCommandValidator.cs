namespace Application.Property.Delete;

internal sealed class DeletePropertyCommandValidator : AbstractValidator<DeletePropertyCommand>
{
    public DeletePropertyCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(PropertyErrorCodes.Delete.MissingId);
    }
}