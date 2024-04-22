namespace Application.Abstractions.Authentication;

public interface ICurrentUserService
{
    string? Id { get; }
    string? Role { get; }
    string? Email { get; }
}