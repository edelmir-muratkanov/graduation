namespace Api.Shared.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserRole { get; }
}