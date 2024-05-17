namespace Application.Project.AddMethods;

/// <summary>
/// Валидатор для команды <see cref="AddProjectMethodsCommand"/>.
/// </summary>
internal sealed class AddProjectMethodsCommandValidator : AbstractValidator<AddProjectMethodsCommand>
{
    public AddProjectMethodsCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotEmpty().WithMessage("Идентификатор проекта не может быть пустым.");

        RuleFor(c => c.MethodIds)
            .NotEmpty().WithMessage("Список идентификаторов методов не может быть пустым.");

        RuleForEach(c => c.MethodIds)
            .NotEmpty().WithMessage("Идентификатор метода не может быть пустым.");
    }
}
