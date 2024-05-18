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
        Result<User> result = User.Create(Email, Password);

        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().NotBeEmpty();
        result.Value.Email.Should().NotBeEmpty();
        result.Value.Password.Should().NotBeEmpty();
        result.Value.Role.Should().Be(Role.User);
    }

    [Fact]
    public void Create_Should_RaiseDomainEvent()
    {
        Result<User> result = User.Create(Email, Password);

        result.Value.DomainEvents[0].Should().BeOfType<UserRegisteredDomainEvent>();
    }

    [Theory]
    [InlineData(Role.User)]
    [InlineData(Role.Admin)]
    public void Create_Should_ReturnUserWithSpecifiedRole(Role role)
    {
        Result<User> result = User.Create(Email, Password, role);

        result.Value.Role.Should().Be(role);
    }

    [Fact]
    public void UpdateToken_Should_ReturnSuccess()
    {
        Result<User> result = User.Create(Email, Password);
        result.Value.Token.Should().BeNullOrWhiteSpace();

        const string token = "token";
        result.Value.UpdateToken(token);
        result.Value.Token.Should().NotBeNullOrWhiteSpace();
    }
}
