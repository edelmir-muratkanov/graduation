using Domain.Shared;

namespace Domain.Users;

public static class UserErrors
{
    public static readonly Error EmailAlreadyInUse = new(
        "User.EmailAlreadyInUse",
        "The specified email is already in use");

    public static readonly Func<Guid, Error> NotFound = id => new(
        "User.NotFound",
        $"The user with id = '{id}' was not found");

    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials are invalid");
}