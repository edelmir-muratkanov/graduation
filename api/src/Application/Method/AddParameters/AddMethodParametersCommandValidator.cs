namespace Application.Method.AddParameters;

/// <summary>
/// Валидатор для команды <see cref="AddMethodParametersCommand"/>
/// </summary>
internal class AddMethodParametersCommandValidator : AbstractValidator<AddMethodParametersCommand>
{
    public AddMethodParametersCommandValidator()
    {
        // Правило для проверки наличия идентификатора метода
        RuleFor(c => c.MethodId)
            .NotEmpty().WithMessage("Идентификатор метода не должен быть пустым.");

        // Правило для проверки наличия списка параметров
        RuleFor(c => c.Parameters)
            .NotEmpty().WithMessage("Список параметров не должен быть пустым.");

        // Правило для каждого параметра
        RuleForEach(c => c.Parameters)
            .ChildRules(mp =>
            {
                // Правило для проверки наличия идентификатора свойства параметра
                mp.RuleFor(p => p.PropertyId)
                    .NotEmpty().WithMessage("Идентификатор свойства параметра не должен быть пустым.");

                // Правило для проверки наличия первого значения параметра, если второе значение отсутствует
                mp.RuleFor(p => p.First)
                    .NotEmpty().WithMessage("Первое значение параметра не должно быть пустым, если второе значение отсутствует.")
                    .When(p => p.Second is null);

                // Правило для проверки наличия второго значения параметра, если первое значение отсутствует
                mp.RuleFor(p => p.Second)
                    .NotEmpty().WithMessage("Второе значение параметра не должно быть пустым, если первое значение отсутствует.")
                    .When(p => p.First is null);
            });
    }
}
