using Domain.Calculation;
using FluentAssertions;

namespace Domain.UnitTests.Calculation;

public class BelongingTests
{
    [Theory]
    [InlineData(-2)]
    [InlineData(-1.1)]
    [InlineData(2)]
    [InlineData(1.01)]
    public void Belonging_Should_ThrowArgumentOutOfRangeException_WhenDegreeIsInvalid(double degree)
    {
        Belonging Action() => new(degree);

        FluentActions.Invoking(Action).Should().Throw<ArgumentOutOfRangeException>()
            .Which
            .ParamName.Should().Be("degree");
    }

    [Theory]
    [InlineData(-0.8, Belonging.NotApplicable)]
    [InlineData(-0.5, Belonging.NotSuitable)]
    [InlineData(-0.1, Belonging.LowEfficiency)]
    [InlineData(0.5, Belonging.Applicable)]
    [InlineData(0.8, Belonging.Favorable)]
    public void Belonging_Should_HaveProperStatus_WhenDegreeIsValid(double degree, string status)
    {
        var belonging = new Belonging(degree);

        belonging.Status.Should().Be(status);
    }
}
