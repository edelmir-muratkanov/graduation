using System.Security.Claims;
using Api.Shared.Interfaces;

namespace Api.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? Id => httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.NameIdentifier);

    public string? Role => httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.Role);

    public string? Email => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
}