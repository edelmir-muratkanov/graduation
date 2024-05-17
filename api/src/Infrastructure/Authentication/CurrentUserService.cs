using System.Security.Claims;
using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

/// <summary>
/// Сервис для получения информации о текущем пользователе.
/// </summary>
/// <param name="httpContextAccessor">Объект для доступа к текущему HTTP-контексту.</param>
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    /// <inheritdoc/>
    public string? Id => httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.NameIdentifier);

    /// <inheritdoc/>
    public string? Role => httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.Role);

    /// <inheritdoc/>
    public string? Email => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
}
