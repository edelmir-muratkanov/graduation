namespace Application.Method.RemoveParameter;

internal class RemoveMethodParameterCommandValidator : AbstractValidator<RemoveMethodParameterCommand>
{
    public RemoveMethodParameterCommandValidator()
    {
        RuleFor(c => c.MethodId)
            .NotEmpty().WithErrorCode(MethodErrorCodes.RemoveParameter.MissingMethodId);

        RuleFor(c => c.ParameterId)
            .NotEmpty().WithErrorCode(MethodErrorCodes.RemoveParameter.MissingParameterId);
    }
}
