namespace Application.Project.RemoveMethod;

internal class RemoveProjectMethodCommandValidator : AbstractValidator<RemoveProjectMethodCommand>
{
    public RemoveProjectMethodCommandValidator()
    {
        RuleFor(c => c.MethodId)
            .NotEmpty();

        RuleFor(c => c.ProjectId)
            .NotEmpty();
    }
}