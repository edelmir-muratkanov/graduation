using Domain.Users;
using Domain.Users.Events;
using FluentAssertions;
using Shared.Results;

namespace Domain.UnitTests.Users;

public class UserTests
{
    private const string Email = "admin@gmail.com";
    private const string Password = "123456";

    [Fact]
    public void Create_Should_ReturnSuccessResult()
    {
        Result<Domain.Users.User> result = Domain.Users.User.Create(Email, Password);

        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Create_Should_RaiseDomainEvent()
    {
        Result<Domain.Users.User> result = Domain.Users.User.Create(Email, Password);

        result.Value.DomainEvents[0].Should().BeOfType<UserRegisteredDomainEvent>();
    }

    [Theory]
    [InlineData(Role.User)]
    [InlineData(Role.Admin)]
    public void Create_Should_ReturnUserWithSpecifiedRole(Role role)
    {
        Result<Domain.Users.User> result = Domain.Users.User.Create(Email, Password, role);

        result.Value.Role.Should().Be(role);
    }

    [Fact]
    public void UpdateToken_Should_ReturnSuccess()
    {
        Result<Domain.Users.User> result = Domain.Users.User.Create(Email, Password);
        result.Value.Token.Should().BeNullOrWhiteSpace();

        const string token = "token";
        result.Value.UpdateToken(token);
        result.Value.Token.Should().NotBeNullOrWhiteSpace();
    }
}
