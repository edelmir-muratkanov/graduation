namespace Application.Method.Update;

internal sealed class UpdateMethodCommandValidator : AbstractValidator<UpdateMethodCommand>
{
    public UpdateMethodCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(MethodErrorCodes.Update.MissingId);

        RuleFor(c => c.Name)
            .MaximumLength(255).WithErrorCode(MethodErrorCodes.Update.LongName);

        RuleFor(c => c.CollectorTypes)
            .ForEach(ct =>
                ct.IsInEnum().WithErrorCode(MethodErrorCodes.Update.InvalidCollectorType));
    }
}
