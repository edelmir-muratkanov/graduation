namespace Api.Shared.Interfaces;

public interface ICurrentUserService
{
    string? Id { get; }
    string? Role { get; }
    string? Email { get; }
}