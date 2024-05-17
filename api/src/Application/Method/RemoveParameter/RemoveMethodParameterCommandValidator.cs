namespace Application.Method.RemoveParameter;

/// <summary>
/// Валидатор для команды удаления параметра метода.
/// </summary>
internal class RemoveMethodParameterCommandValidator : AbstractValidator<RemoveMethodParameterCommand>
{
    public RemoveMethodParameterCommandValidator()
    {
        // Правило для проверки наличия идентификатора метода
        RuleFor(c => c.MethodId)
            .NotEmpty().WithMessage("Идентификатор метода не может быть пустым.");


        // Правило для проверки наличия идентификатора параметра
        RuleFor(c => c.ParameterId)
            .NotEmpty().WithMessage("Идентификатор параметра не может быть пустым.");
    }
}
