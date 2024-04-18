using System.Security.Claims;
using Api.Shared.Interfaces;

namespace Api.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? UserId => httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserRole => httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.Role);
}