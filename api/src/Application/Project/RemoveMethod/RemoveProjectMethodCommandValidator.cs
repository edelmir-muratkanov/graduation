namespace Application.Project.RemoveMethod;

/// <summary>
/// Валидатор для команды <see cref="RemoveProjectMethodCommand"/>
/// </summary>
internal class RemoveProjectMethodCommandValidator : AbstractValidator<RemoveProjectMethodCommand>
{
    public RemoveProjectMethodCommandValidator()
    {
        // Правило для проверки наличия идентификатора метода
        RuleFor(c => c.MethodId)
            .NotEmpty().WithMessage("Идентификатор метода не может быть пустым.");

        // Правило для проверки наличия идентификатора проекта
        RuleFor(c => c.ProjectId)
            .NotEmpty().WithMessage("Идентификатор проекта не может быть пустым.");
    }
}
