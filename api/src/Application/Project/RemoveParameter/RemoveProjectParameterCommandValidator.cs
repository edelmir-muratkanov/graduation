namespace Application.Project.RemoveParameter;

internal class RemoveProjectParameterCommandValidator : AbstractValidator<RemoveProjectParameterCommand>
{
    public RemoveProjectParameterCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotEmpty();

        RuleFor(c => c.ParameterId)
            .NotEmpty();
    }
}
