using Api.Domain.Users;

namespace Api.Shared.Interfaces;

public interface IJwtTokenProvider
{
    string Generate(User user);
    string GenerateRefreshToken();
    Task<string?> GetUserFromToken(string token);
}