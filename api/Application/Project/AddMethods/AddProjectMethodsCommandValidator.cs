namespace Application.Project.AddMethods;

internal sealed class AddProjectMethodsCommandValidator : AbstractValidator<AddProjectMethodsCommand>
{
    public AddProjectMethodsCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotEmpty();

        RuleFor(c => c.MethodIds)
            .NotEmpty();

        RuleForEach(c => c.MethodIds)
            .NotEmpty();
    }
}