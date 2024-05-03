using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Properties;

namespace Application.Method.AddParameters;

internal class AddMethodParametersCommandHandler(
    IMethodRepository methodRepository,
    IPropertyRepository propertyRepository,
    IMethodParameterRepository methodParameterRepository,
    ICalculationRepository calculationRepository,
    ICalculationService calculationService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddMethodParametersCommand>
{
    public async Task<Result> Handle(AddMethodParametersCommand request, CancellationToken cancellationToken)
    {
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(request.MethodId, cancellationToken);

        if (method is null)
        {
            return Result.Failure(MethodErrors.NotFound);
        }

        foreach (MethodParameter? parameter in request.Parameters)
        {
            if (!await propertyRepository.Exists(parameter.PropertyId))
            {
                return Result.Failure(MethodErrors.NotFoundParameter);
            }
        }

        var results = request.Parameters.Select(parameterRequest =>
                method.AddParameter(
                    parameterRequest.PropertyId,
                    parameterRequest.First,
                    parameterRequest.Second))
            .ToList();

        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        List<Calculation> calculations = await calculationRepository.GetByMethodAsync(method.Id, cancellationToken);

        foreach (Calculation calculation in calculations)
        {
            Domain.Projects.Project? project = await projectRepository
                .GetByIdAsync(calculation.ProjectId, cancellationToken);

            Result result = await calculationService.Update(project!, method);

            if (result.IsFailure)
            {
                return result;
            }
        }

        methodParameterRepository.InsertRange(results.Select(r => r.Value));
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
