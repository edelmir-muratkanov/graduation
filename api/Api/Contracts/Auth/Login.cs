namespace Api.Contracts.Auth;

public record LoginRequest(string Email, string Password);

public record LoginResponse(Guid Id, string Email, string Role, string Token);