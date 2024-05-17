namespace Application.Project.AddParameters;

/// <summary>
/// Валидатор для команды <see cref="AddProjectParametersCommand"/>.
/// </summary>
internal class AddProjectParametersCommandValidator : AbstractValidator<AddProjectParametersCommand>
{
    public AddProjectParametersCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotEmpty().WithMessage("Идентификатор проекта не может быть пустым.");

        RuleFor(c => c.Parameters)
            .NotEmpty().WithMessage("Список параметров не может быть пустым.");

        RuleForEach(c => c.Parameters)
            .NotEmpty().WithMessage("Параметр не может быть пустым.")
            .ChildRules(rules =>
            {
                rules.RuleFor(p => p.PropertyId)
                    .NotEmpty().WithMessage("Идентификатор свойства не может быть пустым.");
                rules.RuleFor(p => p.Value)
                    .NotEmpty().WithMessage("Значение параметра не может быть пустым.");
            });
    }
}
