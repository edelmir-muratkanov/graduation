namespace Application.Property.GetById;

internal class GetPropertyByIdQueryValidator : AbstractValidator<GetPropertyByIdQuery>
{
    public GetPropertyByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithErrorCode(PropertyErrorCodes.GetById.MissingId);
    }
}
