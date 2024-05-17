namespace Application.Property.Create;

/// <summary>
/// Валидатор для команды <see cref="CreatePropertyCommand"/>
/// </summary>
internal sealed class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        // Правило для проверки непустого названия свойства
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Необходимо указать название свойства.")
            .MaximumLength(255).WithMessage("Длина названия свойства не должна превышать 255 символов.");

        // Правило для проверки непустых единиц измерения свойства
        RuleFor(c => c.Unit)
            .NotEmpty().WithMessage("Необходимо указать единицы измерения свойства.")
            .MaximumLength(255).WithMessage("Длина единиц измерения свойства не должна превышать 255 символов.");
    }
}
