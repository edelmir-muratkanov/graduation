namespace Application.Project.AddMembers;

/// <summary>
/// Валидатор для команды <see cref="AddProjectMembersCommand"/>.
/// </summary>
internal sealed class AddProjectMemberCommandValidator : AbstractValidator<AddProjectMembersCommand>
{
    public AddProjectMemberCommandValidator()
    {
        // Правило для проверки наличия идентификатора проекта
        RuleFor(c => c.ProjectId)
            .NotEmpty().WithMessage("Идентификатор проекта не может быть пустым.");

        // Правило для проверки наличия списка идентификаторов участников
        RuleFor(c => c.MemberIds)
            .NotEmpty().WithMessage("Список идентификаторов участников не может быть пустым.");

        // Правило для проверки наличия каждого идентификатора участника
        RuleForEach(c => c.MemberIds)
            .NotEmpty().WithMessage("Идентификатор участника не может быть пустым.");
    }
}
