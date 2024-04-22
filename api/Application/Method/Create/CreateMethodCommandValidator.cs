namespace Application.Method.Create;

internal sealed class CreateMethodCommandValidator : AbstractValidator<CreateMethodCommand>
{
    public CreateMethodCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingName)
            .MaximumLength(255).WithErrorCode(MethodErrorCodes.Create.LongName);

        RuleFor(c => c.CollectorTypes)
            .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingCollectorTypes)
            .ForEach(ct =>
                ct.IsInEnum().WithErrorCode(MethodErrorCodes.Create.InvalidCollectorType));

        RuleFor(c => c.Parameters)
            .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingParameters)
            .ForEach(mp => mp
                .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingParameter));

        RuleForEach(c => c.Parameters)
            .ChildRules(mp =>
            {
                mp.RuleFor(p => p.PropertyId)
                    .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingProperty);

                mp.RuleFor(p => p.FirstParameters)
                    .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingFirstOrSecondParameters)
                    .When(p => p.SecondParameters is null);

                mp.RuleFor(p => p.SecondParameters)
                    .NotEmpty().WithErrorCode(MethodErrorCodes.Create.MissingFirstOrSecondParameters)
                    .When(p => p.FirstParameters is null);
            });
    }
}