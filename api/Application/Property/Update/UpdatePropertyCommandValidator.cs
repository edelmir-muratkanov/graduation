namespace Application.Property.Update;

internal sealed class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
{
    public UpdatePropertyCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(PropertyErrorCodes.Update.MissingId);
    }
}