using Domain.Calculation;
using Domain.Methods;
using Domain.Properties;
using FluentAssertions;
using NSubstitute;

namespace Domain.UnitTests.Calculation;

public class CalculationServiceTests
{
    private readonly ICalculationRepository _calculationRepositoryMock;
    private readonly IPropertyRepository _propertyRepositoryMock;
    private readonly CalculationService _calculationService;


    public CalculationServiceTests()
    {
        _calculationRepositoryMock = Substitute.For<ICalculationRepository>();
        _propertyRepositoryMock = Substitute.For<IPropertyRepository>();
        _calculationService = new CalculationService();
    }

    
    
    [Fact]
    public void CalculateBelongingDegree_Should_ReturnProperBelonging_When_ProjectParameterAbsent()
    {
        Belonging result =_calculationService.CalculateBelongingDegree(null, null, null);

        result.Degree.Should().Be(-1);
    }
    
    [Fact]
    public void CalculateBelongingDegree_Should_ReturnInvalidOperationException_When_FirstAndSecondParametersAbsent()
    {
        Belonging Action() => _calculationService.CalculateBelongingDegree(1, null, null);

        FluentActions.Invoking(Action).Should().Throw<InvalidOperationException>();

    }
    
    [Fact]
    public void CalculateBelongingDegree_Should_ReturnProperBelonging_When_MethodFirstParameterAbsent()
    {
        var second = new ParameterValueGroup(35, 36, 37);
        Belonging result =_calculationService.CalculateBelongingDegree(2.31, null, second);
        result.Degree.Should().Be(-1);
    }
    
    [Fact]
    public void CalculateBelongingDegree_Should_ReturnProperBelonging_When_MethodSecondParameterAbsent()
    {
        var first = new ParameterValueGroup(0.03, 0.07, 0.1);
        Belonging result =_calculationService.CalculateBelongingDegree(0.19, first, null);
        result.Degree.Should().Be(1);
    }
    
    [Fact]
    public void CalculateBelongingDegree_Should_ReturnProperBelonging_When_MethodParametersPresent()
    {
        var first = new ParameterValueGroup(1.5, 5, 10);
        var second = new ParameterValueGroup(200, 4000, 4500);
        Belonging result =_calculationService.CalculateBelongingDegree(21.5, first, second);
        result.Degree.Should().Be(1);
    }
}
