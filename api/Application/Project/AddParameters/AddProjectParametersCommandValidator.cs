namespace Application.Project.AddParameters;

internal class AddProjectParametersCommandValidator : AbstractValidator<AddProjectParametersCommand>
{
    public AddProjectParametersCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotEmpty();

        RuleFor(c => c.Parameters)
            .NotEmpty();

        RuleForEach(c => c.Parameters)
            .NotEmpty()
            .ChildRules(rules =>
            {
                rules.RuleFor(p => p.PropertyId)
                    .NotEmpty();
                rules.RuleFor(p => p.Value)
                    .NotEmpty();
            });
    }
}