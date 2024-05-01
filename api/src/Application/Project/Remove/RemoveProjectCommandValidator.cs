namespace Application.Project.Remove;

internal sealed class RemoveProjectCommandValidator : AbstractValidator<RemoveProjectCommand>
{
    public RemoveProjectCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotEmpty();
    }
}
