using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Properties;
using FluentAssertions;
using NSubstitute;
using Shared.Results;

namespace Domain.UnitTests.Calculations;

public class CalculationServiceTests
{
    private readonly CalculationService _calculationService;


    public CalculationServiceTests()
    {
        ICalculationRepository calculationRepositoryMock = Substitute.For<ICalculationRepository>();
        IPropertyRepository propertyRepositoryMock = Substitute.For<IPropertyRepository>();
        _calculationService = new CalculationService(propertyRepositoryMock, calculationRepositoryMock);
    }


    [Fact]
    public void CalculateBelongingDegree_Should_ReturnProperBelonging_When_ProjectParameterAbsent()
    {
        Belonging result = _calculationService.CalculateBelongingDegree(null, null, null);

        result.Degree.Should().Be(-1);
    }

    [Fact]
    public void CalculateBelongingDegree_Should_ReturnInvalidOperationException_When_FirstAndSecondParametersAbsent()
    {
        Belonging Action() => _calculationService.CalculateBelongingDegree(1, null, null);

        FluentActions.Invoking(Action).Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(2.31, 35, 36, 37, 1)]
    [InlineData(2.31, 0.3, 0.5, 1, -1)]
    [InlineData(2.31, 0.2, 0.6, 1, -1)]
    [InlineData(783, 834, 840, 850, 1)]
    [InlineData(25, 10, 20, 30, 0.9878)]
    public void CalculateBelongingDegree_Should_ReturnProperBelonging_When_MethodFirstParameterAbsent(
        double x,
        double min,
        double avg,
        double max,
        double expected)
    {
        var second = new ParameterValueGroup(min, avg, max);
        Belonging result = _calculationService.CalculateBelongingDegree(x, null, second);
        Math.Round(result.Degree, 4).Should().Be(expected);
    }

    [Theory]
    [InlineData(0.67, 0.4, 0.42, 0.43, 1)]
    [InlineData(0.67, 0.75, 0.78, 0.8, -1)]
    [InlineData(0.67, 0.15, 0.17, 0.2, 1)]
    [InlineData(85, 80, 85, 87, 0.5)]
    public void CalculateBelongingDegree_Should_ReturnProperBelonging_When_MethodSecondParameterAbsent(
        double x,
        double min,
        double avg,
        double max,
        double expected)
    {
        var first = new ParameterValueGroup(min, avg, max);
        Belonging result = _calculationService.CalculateBelongingDegree(x, first, null);
        Math.Round(result.Degree, 4).Should().Be(expected);
    }

    [Theory]
    [InlineData(1861, 457, 800, 1000, 3800, 4000, 4075, 1)]
    [InlineData(783, 801, 810, 820, 900, 910, 922, -1)]
    [InlineData(2.31, 0.04, 5, 10, 286, 400, 18000, 0.0813)]
    [InlineData(25, 1, 2, 3, 10, 20, 30, 0.9878)]
    [InlineData(70, 1, 2, 3, 10, 20, 30, -1)]
    public void CalculateBelongingDegree_Should_ReturnProperBelonging_When_MethodParametersPresent(
        double x,
        double min1,
        double avg1,
        double max1,
        double min2,
        double avg2,
        double max2,
        double expected)
    {
        var first = new ParameterValueGroup(min1, avg1, max1);
        var second = new ParameterValueGroup(min2, avg2, max2);
        Belonging result = _calculationService.CalculateBelongingDegree(x, first, second);
        Math.Round(result.Degree, 4).Should().Be(expected);
    }
}
