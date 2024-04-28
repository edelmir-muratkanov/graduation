using Application.Abstractions.Messaging;
using FluentAssertions;
using NetArchTest.Rules;

namespace ArchitectureTests.Infrastructure;

public class InfrastructureTests : BaseTest
{
    [Fact]
    public void QueryHandlers_Should_HaveQueryHandlerPostfix()
    {
        TestResult? result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlers_Should_BeSealed()
    {
        TestResult result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlers_ShouldNot_HaveDependencyOnRepositories()
    {
        TestResult result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .ShouldNot()
            .HaveDependencyOn("Infrastructure.Repository")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Repositories_Should_BeSealed()
    {
        TestResult result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .BeSealed()
            .GetResult();
    }

    [Fact]
    public void Repositories_Should_ImplementInterfacesFromDomain()
    {
        TestResult result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .HaveDependencyOn("Domain")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
    
    
}
