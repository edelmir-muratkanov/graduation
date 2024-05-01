namespace Api.Contracts.Auth;

public record RegisterRequest(string Email, string Password);

public record RegisterResponse(Guid Id, string Email, string Role, string Token);
