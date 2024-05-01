using Domain.Calculation;
using FluentAssertions;
using Shared.Results;

namespace Domain.UnitTests.Calculations;

public class CalculationTests
{
    [Fact]
    public void Create_Should_ReturnCalculation_WhenParametersValid()
    {
        var calculation = Domain.Calculation.Calculation.Create(Guid.NewGuid(), Guid.NewGuid());

        calculation.Should().NotBeNull();
        calculation.Items.Count().Should().Be(0);
        calculation.Belonging.Should().BeNull();
    }

    [Fact]
    public void AddItem_Should_ReturnSuccessResult_WhenItemValidAndNew()
    {
        var calculation = Domain.Calculation.Calculation.Create(Guid.NewGuid(), Guid.NewGuid());

        Result result = calculation.AddItem("name", new Belonging(0.5));

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void AddItem_Should_ReturnFailureResult_WhenItemPresent()
    {
        var calculation = Domain.Calculation.Calculation.Create(Guid.NewGuid(), Guid.NewGuid());
        calculation.AddItem("name", new Belonging(0.5));
        
        Result result = calculation.AddItem("name", new Belonging(0.5));

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CalculationErrors.DuplicateItems);
    }
    
    [Fact]
    public void RemoveItem_Should_ReturnSuccessResult_WhenItemPresent()
    {
        var calculation = Domain.Calculation.Calculation.Create(Guid.NewGuid(), Guid.NewGuid());
        calculation.AddItem("name", new Belonging(0.5));

        Result result = calculation.RemoveItem("name");

        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void RemoveItem_Should_ReturnFailureResult_WhenItemAbsent()
    {
        var calculation = Domain.Calculation.Calculation.Create(Guid.NewGuid(), Guid.NewGuid());

        Result result = calculation.RemoveItem("name");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CalculationErrors.ItemNotFound);
    }

    [Fact]
    public void AddItem_Should_UpdateCalculationBelonging_WhenItemsChanged()
    {
        var calculation = Domain.Calculation.Calculation.Create(Guid.NewGuid(), Guid.NewGuid());
        calculation.AddItem("name", new Belonging(0.5));

        calculation.Belonging.Should().NotBeNull();
        calculation.Belonging!.Degree.Should().Be(0.5);

        calculation.RemoveItem("name");

        calculation.Belonging.Should().BeNull();
    }

   
}
