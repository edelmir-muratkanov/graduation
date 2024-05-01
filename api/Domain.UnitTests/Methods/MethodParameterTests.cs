using Domain.Methods;
using FluentAssertions;
using Shared.Results;

namespace Domain.UnitTests.Methods;

public class MethodParameterTests
{
    private readonly Guid _methodId = Guid.NewGuid();
    private readonly Guid _propertyId = Guid.NewGuid();
    private readonly ParameterValueGroup _first = new(1, 2, 3);
    private readonly ParameterValueGroup _second = new(10, 20, 30);

    [Fact]
    public void Create_Should_ReturnSuccessResult_WhenArgumentsValid()
    {
        Result<MethodParameter> result = MethodParameter.Create(_methodId, _propertyId, _first, _second);

        result.IsSuccess.Should().BeTrue();
        result.Value.MethodId.Should().Be(_methodId);
        result.Value.PropertyId.Should().Be(_propertyId);
        result.Value.FirstParameters.Should().Be(_first);
        result.Value.SecondParameters.Should().Be(_second);
    }

    [Fact]
    public void Create_Should_ReturnFailureResult_WhenFirstAndSecondNull()
    {
        Result<MethodParameter> result = MethodParameter.Create(_methodId, _propertyId, null, null);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MethodParameterErrors.MissingFirstAndSecond);
    }

    [Fact]
    public void Create_Should_GenerateUniqueIdForInstances()
    {
        MethodParameter methodParameter1 = MethodParameter.Create(_methodId, _propertyId, _first, _second).Value;
        MethodParameter methodParameter2 = MethodParameter.Create(_methodId, _propertyId, _first, _second).Value;

        methodParameter1.Id.Should().NotBe(methodParameter2.Id);
    }
}
