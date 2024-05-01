namespace Application.Method.AddParameters;

internal class AddMethodParametersCommandValidator : AbstractValidator<AddMethodParametersCommand>
{
    public AddMethodParametersCommandValidator()
    {
        RuleFor(c => c.MethodId)
            .NotEmpty();

        RuleFor(c => c.Parameters)
            .NotEmpty();

        RuleForEach(c => c.Parameters)
            .ChildRules(mp =>
            {
                mp.RuleFor(p => p.PropertyId)
                    .NotEmpty();

                mp.RuleFor(p => p.First)
                    .NotEmpty()
                    .When(p => p.Second is null);

                mp.RuleFor(p => p.Second)
                    .NotEmpty()
                    .When(p => p.First is null);
            });
    }
}
