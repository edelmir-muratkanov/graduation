namespace Application.Project.Create;

internal class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(ProjectErrorCodes.Create.MissingName);

        RuleFor(c => c.Operator)
            .NotEmpty().WithErrorCode(ProjectErrorCodes.Create.MissingOperator);

        RuleFor(c => c.Country)
            .NotEmpty().WithErrorCode(ProjectErrorCodes.Create.MissingCountry);

        RuleFor(c => c.CollectorType)
            .IsInEnum().WithErrorCode(ProjectErrorCodes.Create.InvalidCollectorType);

        RuleFor(c => c.ProjectType)
            .IsInEnum().WithErrorCode(ProjectErrorCodes.Create.InvalidProjectType);

        RuleFor(c => c.MethodIds)
            .NotEmpty().WithErrorCode(ProjectErrorCodes.Create.MissingMethods);

        RuleFor(c => c.Parameters)
            .NotEmpty().WithErrorCode(ProjectErrorCodes.Create.MissingParameters);

        RuleForEach(c => c.Parameters)
            .NotEmpty().WithErrorCode(ProjectErrorCodes.Create.MissingParameter)
            .ChildRules(rules =>
            {
                rules.RuleFor(p => p.PropertyId)
                    .NotEmpty().WithErrorCode(ProjectErrorCodes.Create.MissingProperty);

                rules.RuleFor(p => p.Value)
                    .NotEmpty().WithErrorCode(ProjectErrorCodes.Create.MissingParameterValue);
            });
    }
}