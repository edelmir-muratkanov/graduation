namespace Application.Method.Delete;

internal sealed class DeleteMethodCommandValidator : AbstractValidator<DeleteMethodCommand>
{
    public DeleteMethodCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(MethodErrorCodes.Delete.MissingId);
    }
}
