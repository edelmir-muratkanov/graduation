﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Authentication;

/// <summary>
/// Сервис для работы с JWT-токенами
/// </summary>
/// <param name="options">Настройки JWT-токенов.</param>
internal sealed class JwtTokenProvider(IOptions<JwtOptions> options) : IJwtTokenProvider
{
    private readonly JwtOptions _jwtOptions = options.Value;

    /// <inheritdoc/>
    public string Generate(User user)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("role", user.Role.ToString())
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessExpiryInMinutes),
            signingCredentials: signingCredentials);

        string? tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }

    /// <inheritdoc/>
    public string GenerateRefreshToken()
    {
        byte[]? randomNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    /// <inheritdoc/>
    public async Task<string?> GetUserFromToken(string token)
    {
        var tokenValidationParameter = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret))
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        TokenValidationResult? result = await tokenHandler.ValidateTokenAsync(token, tokenValidationParameter);

        return result.Claims.First(u => u.Key == ClaimTypes.NameIdentifier).Value.ToString();
    }
}
