using Domain.Calculation;
using FluentAssertions;

namespace Domain.UnitTests.Calculations;

public class CalculationItemTests
{
    [Fact]
    public void Create_Should_CreateItem_When_BelongingIsValid()
    {
        Belonging belonging = new(0.7);

        var calculationItem = CalculationItem.Create(Guid.NewGuid(), "Some name", belonging);

        calculationItem.Should().NotBeNull();
    }
}
