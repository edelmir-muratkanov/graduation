using FluentAssertions;
using NetArchTest.Rules;

namespace ArchitectureTests;

public class LayerTests : BaseTest
{
    [Fact]
    public void Domain_Should_NotHaveAnyDependency()
    {
        TestResult? result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOnAll("Application", "Infrastructure", "Api")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_NotHaveDependencyOnInfrastructure()
    {
        TestResult? result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOnAll("Infrastructure", "Api")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
