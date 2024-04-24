namespace Application.Method.GetMethodById;

public class GetMethodByIdQueryValidator : AbstractValidator<GetMethodByIdQuery>
{
    public GetMethodByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithErrorCode(MethodErrorCodes.GetById.MissingId);
    }
}
