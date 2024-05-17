namespace Application.Project.RemoveParameter;

/// <summary>
/// Валидатор для команды удаления параметра из проекта.
/// </summary>
internal class RemoveProjectParameterCommandValidator : AbstractValidator<RemoveProjectParameterCommand>
{
    public RemoveProjectParameterCommandValidator()
    {
        // Правило для проверки наличия идентификатора проекта
        RuleFor(c => c.ProjectId)
            .NotEmpty().WithMessage("Идентификатор проекта не может быть пустым.");;

        // Правило для проверки наличия идентификатора параметра
        RuleFor(c => c.ParameterId)
            .NotEmpty().WithMessage("Идентификатор параметра не может быть пустым.");;
    }
}
