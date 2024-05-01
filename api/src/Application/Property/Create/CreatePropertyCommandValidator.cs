namespace Application.Property.Create;

internal sealed class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(PropertyErrorCodes.Create.MissingName)
            .MaximumLength(255).WithErrorCode(PropertyErrorCodes.Create.LongName);

        RuleFor(c => c.Unit)
            .NotEmpty().WithErrorCode(PropertyErrorCodes.Create.MissingUnit)
            .MaximumLength(255).WithErrorCode(PropertyErrorCodes.Create.LongUnit);
    }
}
