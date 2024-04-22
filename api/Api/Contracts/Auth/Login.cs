namespace Api.Contracts.Auth;

public record LoginResponse(Guid Id, string Email, string Role, string Token);