namespace Application.Project.Update;

internal sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();

        RuleFor(c => c.Name)
            .MaximumLength(255);

        RuleFor(c => c.Operator)
            .MaximumLength(255);

        RuleFor(c => c.Country)
            .MaximumLength(255);

        RuleFor(c => c.ProjectType)
            .IsInEnum();

        RuleFor(c => c.CollectorType)
            .IsInEnum();
    }
}
