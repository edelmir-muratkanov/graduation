using System.Security.Principal;
using Api.Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace Api.Shared.Interfaces;

public interface IJwtTokenProvider
{
    string Generate(User user);
    string GenerateRefreshToken();
    Task<string?> GetUserFromToken(string token);
}