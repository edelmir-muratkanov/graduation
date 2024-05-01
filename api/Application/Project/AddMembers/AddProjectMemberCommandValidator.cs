namespace Application.Project.AddMembers;

internal sealed class AddProjectMemberCommandValidator : AbstractValidator<AddProjectMembersCommand>
{
    public AddProjectMemberCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotEmpty();

        RuleFor(c => c.MemberIds)
            .NotEmpty();

        RuleForEach(c => c.MemberIds)
            .NotEmpty();
    }
}
